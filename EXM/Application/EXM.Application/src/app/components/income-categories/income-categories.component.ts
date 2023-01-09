import {
  Component,
  OnInit
} from "@angular/core";
import {
  FormBuilder,
  FormControl,
  FormGroup,
  Validators
} from "@angular/forms";
import { Title } from "@angular/platform-browser";

import { EmitActionsConstants } from "../../constants";
import { MainLayoutEmit } from "../../emits";
import {
  IncomeCategoryViewer,
  PagedResultsResponse
} from "../../models";
import { RootScope } from "../../scopes";
import { IncomeCategoriesService } from "../../services";

declare var bootstrap: any;

@Component({
  selector: 'app-income-categories',
  templateUrl: './income-categories.component.html',
  styleUrls: ['./income-categories.component.css']
})
export class IncomeCategoriesComponent implements OnInit {

  dtOptions: any = {};
  language = '';
  translations: any = {};
  form: FormGroup = new FormGroup({
    Name: new FormControl('')
  });
  modal: any;
  loading: boolean = false;
  submitted: boolean = false;
  formError: boolean = false;

  // Constructor
  constructor(
    private formBuilder: FormBuilder,
    private mainLayoutEmit: MainLayoutEmit,
    private rootScope: RootScope,
    private incomeCategoriesService: IncomeCategoriesService,
    private titleService: Title
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
      pageLength: 10,
      lengthChange: false,
      paging: true,
      ajax: (dataTablesParameters: any, callback: any) => {
        const page = parseInt(dataTablesParameters.start) / parseInt(dataTablesParameters.length) + 1;
        this.incomeCategoriesService.getAll(page, dataTablesParameters.length, dataTablesParameters.search.value).subscribe((resp: PagedResultsResponse<IncomeCategoryViewer>) => {
          if (resp.succeeded) {
            callback({
              recordsTotal: resp.totalCount,
              recordsFiltered: resp.totalCount,
              data: resp.data
            });
          }
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
        }
      ],
      dom: 'Bfrtip',
      buttons: [
        {
          className: 'btn btn-primary',
          text: this.translations.incomeCategories.table.buttons.add,
          key: '1',
          action: (e: any, dt: any, node: any, config: any) => {
            this.form = this.formBuilder.group({
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

  get f() {
    return this.form?.controls;
  }

  onSubmit() {
    this.formError = false;
    this.submitted = true;

    if (this.form.invalid) {
      return;
    }

    //this.loader.startLoader('loader-login');

    //const returnUrl = this.activatedRoute.snapshot.queryParams['returnUrl'] || '';
    //this.authService
    //  .login(this.f['Email'].value, this.f['Password'].value, this.f['RememberMe'].value)
    //  .pipe(
    //    finalize(() => {
    //      this.loader.stopLoader('loader-login');
    //      this.Submitted = false;
    //    })
    //  )
    //  .subscribe(
    //    (r: Result<TokenResponse>) => {
    //      if (r.succeeded) {
    //        this.router.navigate([returnUrl]);
    //      } else {
    //        this.ErrorsList = new Array<string>();

    //        r.messages.forEach((value: string) => {
    //          this.ErrorsList.push(this.Translations[value]);
    //        });

    //        this.LoginError = true;
    //      }
    //    },
    //    (error: any) => {
    //      this.LoginError = error;
    //    }
    //  );
  }
}
