using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Streetcode.BLL.DTO.News
{
    public class NewsDtooWithUrls
    {
        public NewsDtoo News { get; set; } = new NewsDtoo();

        public string? PrevNewsUrl { get; set; }

        public string? NextNewsUrl { get; set; }

        public RandomNewsDtoo? RandomNews { get; set; } = new RandomNewsDtoo();
    }
}
