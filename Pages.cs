using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace telegram_bot
{
    public enum Pages // TODO: мб стоит сделать отдельный класс и список для него
    {
        // все страницы
        main,
        edit_profile,
        sent_likes,
        show_people,
        recieved_likes,
        matches,
        settings,
        delete_all_data,
        set_search_filters,
        notofocations
    }
}
