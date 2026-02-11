using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dapper;
using DHAFacilitationAPIs.Application.Common.Interfaces;
using DHAFacilitationAPIs.Application.ViewModels;
using DHAFacilitationAPIs.Infrastructure.Data;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace DHAFacilitationAPIs.Infrastructure.Service;
public class SmsService : ISmsService
{
    private readonly HttpClient _httpClient;
    private readonly IProcedureService _sp;

    public SmsService(HttpClient httpClient, IConfiguration config, IProcedureService sp)
    {
        _httpClient = httpClient;
        _sp = sp;
    }

    public async Task<string> SendSmsAsync(string cellnumber, string msg)
    {
        // 1) build your URL
        string url = string.Format("https://api.veevotech.com/v3/sendsms?hash=4186056c9b55fa16d3006446d1a5d7c0&receivernum=+{0}&textmessage={1}", cellnumber, msg);

        // 2) fire the request
        var resp = await _httpClient.GetAsync(url);
        var body = await resp.Content.ReadAsStringAsync();

        // 3) deserialize & log
        var result = JsonConvert.DeserializeObject<SMSLogVM>(body);
        if (result is null)
            throw new InvalidOperationException("Empty SMS provider response");

        if (result.STATUS.Contains("ERROR", StringComparison.OrdinalIgnoreCase))
        {
            var p = new DynamicParameters();
            p.Add("@Status", result.STATUS, DbType.String, size: 50);
            p.Add("@Message_id", result.MESSAGE_ID, DbType.String, size: 50);
            p.Add("@Country_supported", result.COUNTRY_SUPPORTED, DbType.String, size: 50);
            p.Add("@Country_code", result.COUNTRY_CODE, DbType.String, size: 10);
            p.Add("@Country_iso", result.COUNTRY_ISO, DbType.String, size: 10);
            p.Add("@Network_name", result.NETWORK_NAME, DbType.String, size: 50);
            p.Add("@Receiver_number", result.RECEIVER_NUMBER, DbType.String, size: 20);
            p.Add("@Error_filter", result.ERROR_FILTER, DbType.String, size: 100);
            p.Add("@Error_code", result.ERROR_CODE, DbType.String, size: 20);
            p.Add("@Error_description", result.ERROR_DESCRIPTION, DbType.String, size: 200);
            p.Add("@Charged_balance", result.CHARGED_BALANCE, DbType.String, size: 20);
            p.Add("@charging_unit", result.CHARGING_UNIT, DbType.String, size: 20);

            // call the proc
            await _sp.ExecuteAsync(
                "AddSMSLog",
                p,
                cancellationToken: CancellationToken.None
            );

            return $"{result.STATUS} {result.ERROR_FILTER} {result.ERROR_DESCRIPTION}";
        }

        return result.STATUS;
    }
}
