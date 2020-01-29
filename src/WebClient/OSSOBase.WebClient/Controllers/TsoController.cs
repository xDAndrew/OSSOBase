using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using OSSOBase.WebClient.Contract.Responses;

namespace OSSOBase.WebClient.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TsoController : ControllerBase
    {
        [HttpGet]
        public IEnumerable<ShortTsoItem> GetTsoItems()
        {
            return new List<ShortTsoItem>
            {
                new ShortTsoItem
                {
                    Id = 1, 
                    Owner = "Andrei Ivanou",
                    Address = "Рокоссовского, 148-86",
                    ObjectName = "Моя хатка",
                    User = "Andrei",
                    UuAmount = 1.8
                },
                new ShortTsoItem
                {
                    Id = 2,
                    Owner = "Sergei Ivanou",
                    Address = "Рокоссовского, 148-86",
                    ObjectName = "Моя хатка",
                    User = "Andrei",
                    UuAmount = 1.7
                },
                new ShortTsoItem
                {
                    Id = 3,
                    Owner = "Ivan Ivanou",
                    Address = "Рокоссовского, 148-80",
                    ObjectName = "Моя хатка 1",
                    User = "Andrei",
                    UuAmount = 1.6
                },
                new ShortTsoItem
                {
                    Id = 4,
                    Owner = "Petr Ivanou",
                    Address = "Рокоссовского, 148-81",
                    ObjectName = "Моя хатка 2",
                    User = "Andrei",
                    UuAmount = 1.5
                },new ShortTsoItem
                {
                    Id = 5,
                    Owner = "Vlad Ivanou",
                    Address = "Рокоссовского, 148-82",
                    ObjectName = "Моя хатка 3",
                    User = "Andrei",
                    UuAmount = 1.4
                },new ShortTsoItem
                {
                    Id = 6,
                    Owner = "Ignat Ivanou",
                    Address = "Рокоссовского, 148-83",
                    ObjectName = "Моя хатка 4",
                    User = "Andrei",
                    UuAmount = 1.3
                },new ShortTsoItem
                {
                    Id = 7,
                    Owner = "Tola Ivanou",
                    Address = "Рокоссовского, 148-84",
                    ObjectName = "Моя хатка 5",
                    User = "Andrei",
                    UuAmount = 1.2
                },new ShortTsoItem
                {
                    Id = 8,
                    Owner = "Ola Ivanoua",
                    Address = "Рокоссовского, 148-85",
                    ObjectName = "Моя хатка 6",
                    User = "Andrei",
                    UuAmount = 1.1
                },
                new ShortTsoItem
                {
                    Id = 9,
                    Owner = "Sergei Ivanou",
                    Address = "Рокоссовского, 148-86",
                    ObjectName = "Моя хатка",
                    User = "Andrei",
                    UuAmount = 1.7
                },
                new ShortTsoItem
                {
                    Id = 10,
                    Owner = "Ivan Ivanou",
                    Address = "Рокоссовского, 148-80",
                    ObjectName = "Моя хатка 1",
                    User = "Andrei",
                    UuAmount = 1.6
                },
                new ShortTsoItem
                {
                    Id = 11,
                    Owner = "Petr Ivanou",
                    Address = "Рокоссовского, 148-81",
                    ObjectName = "Моя хатка 2",
                    User = "Andrei",
                    UuAmount = 1.5
                },new ShortTsoItem
                {
                    Id = 12,
                    Owner = "Vlad Ivanou",
                    Address = "Рокоссовского, 148-82",
                    ObjectName = "Моя хатка 3",
                    User = "Andrei",
                    UuAmount = 1.4
                },new ShortTsoItem
                {
                    Id = 13,
                    Owner = "Ignat Ivanou",
                    Address = "Рокоссовского, 148-83",
                    ObjectName = "Моя хатка 4",
                    User = "Andrei",
                    UuAmount = 1.3
                },new ShortTsoItem
                {
                    Id = 14,
                    Owner = "Tola Ivanou",
                    Address = "Рокоссовского, 148-84",
                    ObjectName = "Моя хатка 5",
                    User = "Andrei",
                    UuAmount = 1.2
                },new ShortTsoItem
                {
                    Id = 15,
                    Owner = "Ola Ivanoua",
                    Address = "Рокоссовского, 148-85",
                    ObjectName = "Моя хатка 6",
                    User = "Andrei",
                    UuAmount = 1.1
                },
                new ShortTsoItem
                {
                    Id = 16,
                    Owner = "Sergei Ivanou",
                    Address = "Рокоссовского, 148-86",
                    ObjectName = "Моя хатка",
                    User = "Andrei",
                    UuAmount = 1.7
                },
                new ShortTsoItem
                {
                    Id = 17,
                    Owner = "Ivan Ivanou",
                    Address = "Рокоссовского, 148-80",
                    ObjectName = "Моя хатка 1",
                    User = "Andrei",
                    UuAmount = 1.6
                },
                new ShortTsoItem
                {
                    Id = 18,
                    Owner = "Petr Ivanou",
                    Address = "Рокоссовского, 148-81",
                    ObjectName = "Моя хатка 2",
                    User = "Andrei",
                    UuAmount = 1.5
                },new ShortTsoItem
                {
                    Id = 19,
                    Owner = "Vlad Ivanou",
                    Address = "Рокоссовского, 148-82",
                    ObjectName = "Моя хатка 3",
                    User = "Andrei",
                    UuAmount = 1.4
                },new ShortTsoItem
                {
                    Id = 20,
                    Owner = "Ignat Ivanou",
                    Address = "Рокоссовского, 148-83",
                    ObjectName = "Моя хатка 4",
                    User = "Andrei",
                    UuAmount = 1.3
                },new ShortTsoItem
                {
                    Id = 21,
                    Owner = "Tola Ivanou",
                    Address = "Рокоссовского, 148-84",
                    ObjectName = "Моя хатка 5",
                    User = "Andrei",
                    UuAmount = 1.2
                },new ShortTsoItem
                {
                    Id = 22,
                    Owner = "Ola Ivanoua",
                    Address = "Рокоссовского, 148-85",
                    ObjectName = "Моя хатка 6",
                    User = "Andrei",
                    UuAmount = 1.1
                },
                new ShortTsoItem
                {
                    Id = 23,
                    Owner = "Sergei Ivanou",
                    Address = "Рокоссовского, 148-86",
                    ObjectName = "Моя хатка",
                    User = "Andrei",
                    UuAmount = 1.7
                },
                new ShortTsoItem
                {
                    Id = 24,
                    Owner = "Ivan Ivanou",
                    Address = "Рокоссовского, 148-80",
                    ObjectName = "Моя хатка 1",
                    User = "Andrei",
                    UuAmount = 1.6
                },
                new ShortTsoItem
                {
                    Id = 25,
                    Owner = "Petr Ivanou",
                    Address = "Рокоссовского, 148-81",
                    ObjectName = "Моя хатка 2",
                    User = "Andrei",
                    UuAmount = 1.5
                },new ShortTsoItem
                {
                    Id = 26,
                    Owner = "Vlad Ivanou",
                    Address = "Рокоссовского, 148-82",
                    ObjectName = "Моя хатка 3",
                    User = "Andrei",
                    UuAmount = 1.4
                },new ShortTsoItem
                {
                    Id = 27,
                    Owner = "Ignat Ivanou",
                    Address = "Рокоссовского, 148-83",
                    ObjectName = "Моя хатка 4",
                    User = "Andrei",
                    UuAmount = 1.3
                },new ShortTsoItem
                {
                    Id = 28,
                    Owner = "Tola Ivanou",
                    Address = "Рокоссовского, 148-84",
                    ObjectName = "Моя хатка 5",
                    User = "Andrei",
                    UuAmount = 1.2
                },new ShortTsoItem
                {
                    Id = 29,
                    Owner = "Ola Ivanoua",
                    Address = "Рокоссовского, 148-85",
                    ObjectName = "Моя хатка 6",
                    User = "Andrei",
                    UuAmount = 1.1
                },
                new ShortTsoItem
                {
                    Id = 30,
                    Owner = "Sergei Ivanou",
                    Address = "Рокоссовского, 148-86",
                    ObjectName = "Моя хатка",
                    User = "Andrei",
                    UuAmount = 1.7
                },
                new ShortTsoItem
                {
                    Id = 31,
                    Owner = "Ivan Ivanou",
                    Address = "Рокоссовского, 148-80",
                    ObjectName = "Моя хатка 1",
                    User = "Andrei",
                    UuAmount = 1.6
                },
                new ShortTsoItem
                {
                    Id = 32,
                    Owner = "Petr Ivanou",
                    Address = "Рокоссовского, 148-81",
                    ObjectName = "Моя хатка 2",
                    User = "Andrei",
                    UuAmount = 1.5
                },new ShortTsoItem
                {
                    Id = 33,
                    Owner = "Vlad Ivanou",
                    Address = "Рокоссовского, 148-82",
                    ObjectName = "Моя хатка 3",
                    User = "Andrei",
                    UuAmount = 1.4
                },new ShortTsoItem
                {
                    Id = 34,
                    Owner = "Ignat Ivanou",
                    Address = "Рокоссовского, 148-83",
                    ObjectName = "Моя хатка 4",
                    User = "Andrei",
                    UuAmount = 1.3
                },new ShortTsoItem
                {
                    Id = 35,
                    Owner = "Tola Ivanou",
                    Address = "Рокоссовского, 148-84",
                    ObjectName = "Моя хатка 5",
                    User = "Andrei",
                    UuAmount = 1.2
                },new ShortTsoItem
                {
                    Id = 36,
                    Owner = "Ola Ivanoua",
                    Address = "Рокоссовского, 148-85",
                    ObjectName = "Моя хатка 6",
                    User = "Andrei",
                    UuAmount = 1.1
                },
                new ShortTsoItem
                {
                    Id = 37,
                    Owner = "Sergei Ivanou",
                    Address = "Рокоссовского, 148-86",
                    ObjectName = "Моя хатка",
                    User = "Andrei",
                    UuAmount = 1.7
                },
                new ShortTsoItem
                {
                    Id = 38,
                    Owner = "Ivan Ivanou",
                    Address = "Рокоссовского, 148-80",
                    ObjectName = "Моя хатка 1",
                    User = "Andrei",
                    UuAmount = 1.6
                },
                new ShortTsoItem
                {
                    Id = 39,
                    Owner = "Petr Ivanou",
                    Address = "Рокоссовского, 148-81",
                    ObjectName = "Моя хатка 2",
                    User = "Andrei",
                    UuAmount = 1.5
                },new ShortTsoItem
                {
                    Id = 40,
                    Owner = "Vlad Ivanou",
                    Address = "Рокоссовского, 148-82",
                    ObjectName = "Моя хатка 3",
                    User = "Andrei",
                    UuAmount = 1.4
                },new ShortTsoItem
                {
                    Id = 41,
                    Owner = "Ignat Ivanou",
                    Address = "Рокоссовского, 148-83",
                    ObjectName = "Моя хатка 4",
                    User = "Andrei",
                    UuAmount = 1.3
                },new ShortTsoItem
                {
                    Id = 42,
                    Owner = "Tola Ivanou",
                    Address = "Рокоссовского, 148-84",
                    ObjectName = "Моя хатка 5",
                    User = "Andrei",
                    UuAmount = 1.2
                },new ShortTsoItem
                {
                    Id = 43,
                    Owner = "Ola Ivanoua",
                    Address = "Рокоссовского, 148-85",
                    ObjectName = "Моя хатка 6",
                    User = "Andrei",
                    UuAmount = 1.1
                }
            };
        }
    }
}
