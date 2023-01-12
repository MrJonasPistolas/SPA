using EXM.Base.Extensions;
using EXM.Base.Models;
using EXM.Base.Requests.Catalog;
using EXM.Common.Wrapper;
using MediatR;
using Microsoft.Extensions.Localization;
using Microsoft.VisualBasic.FileIO;
using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Data;

namespace EXM.Base.Features.Incomes.Commands.Upload
{
    public partial class UploadIncomesCommand : IRequest<Result<List<IncomeModel>>>
    {
        [Required]
        public ImportIncomesRequest Request { get; set; }
    }

    internal class UploadIncomesCommandHandler : IRequestHandler<UploadIncomesCommand, Result<List<IncomeModel>>>
    {
        private readonly IStringLocalizer<UploadIncomesCommandHandler> _localizer;

        public UploadIncomesCommandHandler(IStringLocalizer<UploadIncomesCommandHandler> localizer)
        {
            _localizer = localizer;
        }

        public async Task<Result<List<IncomeModel>>> Handle(UploadIncomesCommand command, CancellationToken cancellationToken)
        {
            DataTable csvData = new DataTable();
            string jsonString = string.Empty;

            try
            {
                using (TextFieldParser csvReader = new TextFieldParser(command.Request.Data.ToStream()))
                {
                    csvReader.SetDelimiters(new string[] { ";" });
                    csvReader.HasFieldsEnclosedInQuotes = true;
                    string[] colFields;
                    bool tableCreated = false;
                    while (tableCreated == false)
                    {
                        colFields = csvReader.ReadFields();
                        foreach (string column in colFields)
                        {
                            DataColumn datecolumn = new DataColumn(column);
                            datecolumn.AllowDBNull = true;
                            csvData.Columns.Add(datecolumn);
                        }
                        tableCreated = true;
                    }
                    while (!csvReader.EndOfData)
                    {
                        csvData.Rows.Add(csvReader.ReadFields());
                    }
                }
            }
            catch
            {
                return await Result<List<IncomeModel>>.FailAsync(_localizer["Incomes Not Found!"]);
            }

            jsonString = JsonConvert.SerializeObject(csvData);

            var data = JsonConvert.DeserializeObject<List<IncomeModel>>(jsonString);

            return await Result<List<IncomeModel>>.SuccessAsync(data, _localizer["Incomes Uploaded"]);
        }
    }
}
