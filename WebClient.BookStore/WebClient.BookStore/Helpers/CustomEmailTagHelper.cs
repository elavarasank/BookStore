﻿using Microsoft.AspNetCore.Razor.TagHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace WebClient.BookStore.Helpers
{
    public class CustomEmailTagHelper :TagHelper
    {   
        public string MyEmail { get; set; }
        public override void Process(TagHelperContext context, TagHelperOutput output) {
            output.TagName = "a";
            output.Attributes.SetAttribute("href",$"mailto:{MyEmail}");
            output.Attributes.Add("Id", "mailId");
            output.Content.SetContent("mail to");
        }
    }
}
