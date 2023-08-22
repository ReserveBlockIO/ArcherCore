using ArcherCore.Extensions;
using ArcherCore.Logging;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ArcherCore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ArcherController : ControllerBase
    {
        /// <summary>
        /// Check Status of API
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "Archer", "API V1.0" };
        }

        [HttpGet("GetLogsFromText")]
        public async Task<string> GetLogsFromText()
        {
            var logs = await LoggingUtilities.GetLogFromText();

            if(logs != null)
            {
                return JsonConvert.SerializeObject(new { Success = true, Logs = logs }, Formatting.Indented);
            }
            else
            {
                return JsonConvert.SerializeObject(new { Success = false, Logs = "" }, Formatting.Indented);
            }
        }

        /// <summary>
        /// First to the Egg!
        /// Congrats you have found an easter egg!
        /// </summary>
        /// <returns></returns>
        [HttpGet("Snake")]
        public async Task<ContentResult> Snake()
        {
            var gCompressed = @"H4sIAAAAAAAACrVZW28bRRSeZyT+gwkCOyRxYhoKtE4kKEEggUCFF1T1IfElMTi2u+s2XlX573zfOTs7l91xtoqrle2dmXO/zZnx
                                0HxmfjJ/mBfmb/OP+dNcmI65MWtza+bm3HxqPjHDhvHEXJpxOe7gGQJihs8cK+cYHQdjB5VjrohmLfVDvF+ZJegWeHtfrSvMBPSu
                                BfYZxgNzgucL8zyAuoVUGaBmZiFQJ976vbzZUYrPFSiMzH+gkQHiLeiMhdIV5NOVkOMYvHKzktVCIKei3SaCu8Qs5V+YI7HLBLLm
                                Aj/C+0JmsgjnX/CnvWagWQBvBIkUkt9pXNWUbyPwXZh3+M4bNF0Cayy4atEVpO4AcimyjvF+B3urtE3U6eXYn5wLo2NY2VrHOhdK
                                dif81sA8M3vmVHy7F3k9XCE8Z65B4RZQe2XU+XR9fjlWMmCtQEnnCZPVbHQmPl1i9q3QpXX74DLB74XoaWd/hEa/ArZnupUUXbNf
                                WspRtz7b4Fvp+xwt7RcBHGl+DdqOnk+Tsan+IbWBedrAk5Fr+flZ4MPlgLlERE9KOBcfmzIingL3sJrV+I5n7e8xnpDmO3zPxZaM
                                oQK6MkOXMk9bU+NJoA/tu8CYkdDBOyEzydIpftXG9P1CVkmVEUJd9I2Sky5nijI7M8yPJIvIz0o7LnW0vJ0+jFStHSkdqd0EkUSu
                                WVkVyHVa5vnco5t7svm2cfWH9mG0rQBPu7jcpfXmZZV4ZV43yMN6t0Hs+JCnsnofxcQl6LNKpXz9BPFW97XO+vT4mUp0OZt2yhh+
                                KXHN7LlFZjD+enijtw4rWfcD3vTNGtQyofI7IGirvtTQpfix581mHv2eUPoKn15JuYMK2Sn57cvqQTVW2e+3aKD8ViXdUMI3gKZn
                                1uYHwM6En8X8uYrLXkXDz1nSYP2mnAd4/NwcirdCfrFVUvvXthwPa04fY/qdcfCyzIWe+JqfsBa5Ohyv+JU41s/FdV/8cCAS+bPM
                                Niefv1Ik4IuEDWNetOFJgw1juHrddbpq5NiMbdrpmF95WXua5TjfyqGdfPVuxc/1Js5FSwsQri6f79PH2qBI2CCMmzYypns2H9pV
                                x77kQS6dylRi+31Z00IbH1ZVLeR6n8zXFL9wlzqPKMY1ebvWPt1VVYFSFtD9Zyw15y6o6qncn0Ib0v+r6tPUyuxZWGeoSXdL7bD4
                                rnI4ntaq/kwhMzaOjtAtxOPY2u0lXklNVE6+zCl7TmUHuZAd+gaSx1W/V0FTRu4T7IbjPcrPwXYWslStfdy4nXUsR5+rjUmfNu3C
                                J/QA57/EE/Jthi0adO08EM/cxaztY7xYjrOtvQH3nw46jG+qvbxef5ooFzujHGYXH40ZWtp2TrOSm4sP7SwG4qlZWYHb1AqFp/2a
                                rR6Pt/u8meMrYL1uEQXbsVNxkdq59ExQ91qqxreFd5IpHrvgh7HiiFXc0xaY7FNsbKUisY4T71xNGB8vMz5edoT7fzhyu6bLId2d
                                eHfAXplxz7PPEt+0VCFnHubUuDrbUZ68grJnw6YTE+nXz+W8ZRijwltav3m8eS7kOVp5E/dO9jrqXt8JJlHEqy4r2SMt9fAExxMp
                                rerujNxpkLXiGit6tuA5lZ3TXPL5SjzEHWkSwFJnnisUvit2CaVZgN66PGNSLmpfVHYLeToL2lMzc4LWYUVRvXI8Fouxw+7J3vno
                                vutTDmXx4cmxK++0MjktIBclUEztERiFnQbOmdeDpng7fvFtw42cg+agbWUgJH8LWdH9mnWA8U267Mq1c1MI1niNDXcq30+c/J3W
                                jGP6n12YRljcPTIXVR/62lXeJ+bbqjY31R6Feqijd5XqKJG922tUmM2qHe8gtmlWPwGkdfwuoWPxQToWH6TjpoWOfrTtRtPvd+rN
                                XfnS1rvdaMl7113683HeDE9sthKsRdO1V/vsrSyhHnOPo/fc8f3xUHpFVuQJVvz/OG7Q36hEen7hPbVWIfaWz8znwHTPc7m71vtf
                                2oUy2dtR7jLad9o6yDpKz13j4X3qL8JdfUgsV9PU1+F5V+cGiFpWIb2rHwSyHzdoxdmmO3xi2/+F/gcqZ+CqRhoAAA==";

            var gDecompressed = gCompressed.ToDecompress();

            return base.Content(gDecompressed, "text/html");
        }
    }
}
