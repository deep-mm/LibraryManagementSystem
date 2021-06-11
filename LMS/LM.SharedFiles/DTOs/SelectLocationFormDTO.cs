using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace LMS.SharedFiles.DTOs
{
    public class SelectLocationFormDTO
    {
        public int libraryId { get; set; }

        public List<SelectListItem> selectListItems { get; set; }
    }
}
