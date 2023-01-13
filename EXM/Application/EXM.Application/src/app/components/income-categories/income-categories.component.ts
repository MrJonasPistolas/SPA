import {
  AfterViewInit,
  Component,
  OnDestroy,
  OnInit,
  ViewChild
} from "@angular/core";
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators
} from "@angular/forms";
import { Title } from "@angular/platform-browser";
import { Subject } from "rxjs";

import { DataTableDirective } from "angular-datatables";
import { NgxUiLoaderService } from "ngx-ui-loader";

import { EmitActionsConstants } from "../../constants";
import { MainLayoutEmit } from "../../emits";
import {
  IncomeCategoryRequest,
  IncomeCategoryViewer,
  ResultResponse
} from "../../models";
import { RootScope } from "../../scopes";
import { IncomeCategoriesService } from "../../services";

declare var $: any;
declare var bootstrap: any;

@Component({
  selector: 'app-income-categories',
  templateUrl: './income-categories.component.html',
  styleUrls: ['./income-categories.component.css']
})
export class IncomeCategoriesComponent implements AfterViewInit, OnDestroy, OnInit {
  @ViewChild(DataTableDirective, { static: false }) dtElement: DataTableDirective | undefined;

  dtOptions: any = {};
  dtTrigger: any = new Subject();
  //dtOptions: DataTables.Settings = {};
  language = '';
  translations: any = {};
  form: FormGroup = new FormGroup({
    Id: new FormControl('0'),
    Name: new FormControl('')
  });
  modal: any;
  loading: boolean = false;
  submitted: boolean = false;
  formError: boolean = false;
  upsertMode: boolean = false;
  deleteMode: boolean = false;

  // Constructor
  constructor(
    private formBuilder: FormBuilder,
    private mainLayoutEmit: MainLayoutEmit,
    private rootScope: RootScope,
    private incomeCategoriesService: IncomeCategoriesService,
    private titleService: Title,
    private loader: NgxUiLoaderService
  ) {
    this.translations = this.rootScope.GetTranslations();
    this.titleService.setTitle(this.translations.incomeCategories.pageTitle);
    this.mainLayoutEmit.emitChange({
      Action: EmitActionsConstants.BreadcrumbChanged,
      Data: [
        {
          href: '/',
          title: this.translations.incomeCategories.breadcrumb.home,
          isActive: false
        },
        {
          href: '',
          title: this.translations.incomeCategories.breadcrumb.admin,
          isActive: false
        },
        {
          href: '',
          title: this.translations.incomeCategories.breadcrumb.incomeCategories,
          isActive: true
        }
      ]
    });

    this.mainLayoutEmit.emitChange({
      Action: EmitActionsConstants.PageTitleChanged,
      Data: this.translations.incomeCategories.title
    });

    this.language = this.rootScope.GetLanguage();
  }

  ngOnInit(): void {

    this.dtOptions = {
      language: {
        url: `/assets/dt/${this.language.toLowerCase()}.json`
      },
      processing: true,
      serverSide: true,
      pageLength: 8,
      lengthChange: false,
      paging: true,
      ajax: (dataTablesParameters: any, callback: any) => {
        const page = parseInt(dataTablesParameters.start) / parseInt(dataTablesParameters.length) + 1;
        dataTablesParameters.columns.pop();
        dataTablesParameters.columns.pop();
        this.incomeCategoriesService.getPaged(dataTablesParameters).subscribe((resp) => {
          callback({
            recordsTotal: resp.recordsTotal,
            recordsFiltered: resp.recordsFiltered,
            data: resp.data
          });
        });
      },
      columns: [
        {
          title: this.translations.incomeCategories.table.fields.id,
          data: 'id',
          orderable: false
        },
        {
          title: this.translations.incomeCategories.table.fields.name,
          data: 'name'
        },
        {
          title: this.translations.incomeCategories.table.fields.edit,
          render: (data: any, type: any, full: any) => {
            return `<button type="button" class="btn btn-warning btn-sm income-categories-edit" title="${this.translations.incomeCategories.table.fields.edit} - ${full.name}" data-id="${full.id}"><i class="mdi mdi-file-edit"></i> </button>`;
          },
          data: null
        },
        {
          title: this.translations.incomeCategories.table.fields.delete,
          render: (data: any, type: any, full: any) => {
            return `<button type="button" class="btn btn-danger btn-sm income-categories-delete" title="${this.translations.incomeCategories.table.fields.delete} - ${full.name}" data-id="${full.id}"><i class="mdi mdi-delete"></i> </button>`;
          },
          data: null
        }
      ],
      initComplete: (settings: any, json: object) => {
        $('.income-categories-edit').click((event: PointerEvent) => {
          var element: any = event.currentTarget;
          var value: string = element.attributes['data-id'].value;

          this.incomeCategoriesService.getById(value).subscribe((resp: ResultResponse<IncomeCategoryViewer>) => {
            if (resp.succeeded) {
              this.upsertMode = true;
              this.deleteMode = false;

              this.form = this.formBuilder.group({
                Id: [resp.data.id],
                Name: [
                  resp.data.name,
                  [
                    Validators.required,
                    Validators.minLength(4),
                    Validators.maxLength(40)
                  ]
                ]
              });

              this.modal.show();
            }
          });

        });

        $('.income-categories-delete').click((event: PointerEvent) => {
          var element: any = event.currentTarget;
          var value: string = element.attributes['data-id'].value;

          this.upsertMode = false;
          this.deleteMode = true;

          this.form = this.formBuilder.group({
            Id: [value],
            Name: ['']
          });

          this.modal.show();
        });
      },
      dom: 'Bfrtip',
      buttons: [
        {
          className: 'btn btn-primary',
          text: this.translations.incomeCategories.table.buttons.add,
          key: '1',
          action: () => {
            this.submitted = false;
            this.upsertMode = true;
            this.deleteMode = false;
            this.form = this.formBuilder.group({
              Id: ['0'],
              Name: [
                '',
                [
                  Validators.required,
                  Validators.minLength(4),
                  Validators.maxLength(40)
                ]
              ]
            });

            this.modal.show();
          }
        }
      ]
    };

    this.modal = new bootstrap.Modal(
      document.getElementById('modal-income-categories'),
      {
        keyboard: false,
        backdrop: 'static'
      }
    );
  }

  ngAfterViewInit(): void {
    this.dtTrigger.next();
  }

  ngOnDestroy(): void {
    // Do not forget to unsubscribe the event
    this.dtTrigger.unsubscribe();
  }

  rerender(): void {
    if (this.dtElement)
      this.dtElement.dtInstance.then((dtInstance: DataTables.Api) => {
        dtInstance.destroy();
        this.dtTrigger.next();
      });
  }

  get f() {
    return this.form?.controls;
  }

  onSubmit() {
    this.formError = false;
    this.submitted = true;

    if (this.upsertMode) {
      if (this.form.invalid) {
        return;
      }

      let request: IncomeCategoryRequest = {
        id: +this.f['Id'].value,
        name: this.f['Name'].value
      };

      this.loader.startLoader('loader-income-categories');

      this.incomeCategoriesService.upsert(request).subscribe((result: ResultResponse<IncomeCategoryViewer>) => {
        this.modal.hide();
        this.loader.stopLoader('loader-income-categories');
        this.submitted = false;
        this.rerender();
      });
    }

    if (this.deleteMode) {
      var id = this.f['Id'].value;

      this.loader.startLoader('loader-income-categories');

      this.incomeCategoriesService.delete(id).subscribe((result: ResultResponse<boolean>) => {
        this.modal.hide();
        this.loader.stopLoader('loader-income-categories');
        this.submitted = false;
        this.rerender();
      });
    }
    
  }
}
