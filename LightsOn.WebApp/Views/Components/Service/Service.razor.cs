using System.Collections.Immutable;
using LightsOn.WebApp.HttpClients.ApiHttpClient;
using Microsoft.AspNetCore.Components;

namespace LightsOn.WebApp.Views.Components.Service;

public sealed record ServiceMenu(
    ImmutableList<ServiceItem> ServiceItems);
public sealed record ServiceItem(string HeaderText, string MainText, string LowerPriceLimit);
public partial class Service
{
    protected override async Task OnInitializedAsync()
    {
        var serviceMenuItems1 = ImmutableList.Create(
            items: new[]
            {
                new ServiceItem("Підбір дизельного генератора",
                    "- Визначення необхідної потужності ДГУ.\n" +
                                                              "- Вибір ДГУ з врахувань побажань замовника.\n" +
                                                              "- Пропозиція по можливих варіантах ДГУ.",
                    "За домовленістю"),
                new ServiceItem("Встановлення дизельного генератора",
                    "- Виїзд на місце інсталяції ДГУ для попереднього огляду.\n" +
                                                   "- Надання рекомендацій та генерація попередньої пропозиції (кошторису).\n" +
                                                   "- Підбір постачальників та узгодження з замовником вартості та термінів робіт.\n" +
                                                   "- Проведення монтажних та пуско-налагоджувальних робіт (протягнення кабелів, програмування генератора) на об'єкті.\n" +
                                                   "- Тестування обладнання та надання інформації замовником.",
                    "За домовленістю"),
                new ServiceItem("Обслуговування енергетичного обладнання замовника",
                    "- Аналіз енергетичного обладнання (АВР, щити, автомати) та документація об'єкта.\n" +
                    "- Надання рекомендацій по обслуговуванню та модифікації енергетичного обладнання.\n" +
                    "- Узгодження вартості і терміну робіт.\n" +
                    "- Проведення робіт\n" +
                    "- Формування вихідної документації.",
                    "За домовленістю"),
                
                new ServiceItem("Діагностика енергетичного обладнання",
                    "- Аналіз енергетичного обладнання (АВР, щити, автомати) та документація об'єкта.\n" +
                    "- Надання рекомендацій по обслуговуванню та модифікації енергетичного обладнання.\n" +
                    "- Формування вихідної документації.",
                    "У межах Львова: 2000 грн + транспортні витрати\n" +
                    " За межами Львова: по домовленості"),
            });
        
        // var serviceDescriptions = ApiServiceDescription?.GetServiceDescriptionsAsync().Result;
        //
        // var serviceMenuItems = ImmutableList.Create(
        //     items: serviceDescriptions?.ToArray());
        
        var serviceDescriptions =  await ApiServiceDescription?.GetServiceDescriptionsAsync();
        if (serviceDescriptions != null)
        {
            var phoneNumberItems = serviceDescriptions
                .Select(pn => new ServiceItem(pn.HeaderText, pn.MainText, pn.LowerPriceLimit))
                .ToImmutableList();
            ServiceMenu = new ServiceMenu(phoneNumberItems);
        }
    }
    
    public ServiceMenu ServiceMenu { get; set; }
    
    [Inject]
    private IApiHttpClient ApiServiceDescription { get; set; }
}