using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Text;

namespace LMS.SharedFiles.DTOs
{
    public class SelectLocationFormDTO
    {
        public int libraryId { get; set; }

        public List<SelectListItem> selectListItems { get; set; }
    }
}
