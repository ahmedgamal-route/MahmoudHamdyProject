using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace Store.Service.HandleResponse
{
    public class Response
    {
        public Response(int statusCode, string? message = null)
        {
            StatusCode = statusCode;
            Message = message?? GetDefaultMessageStatusCode(StatusCode);
        }
        public int StatusCode { get; set; }
        public string Message { get; set; }
        private string GetDefaultMessageStatusCode(int StatusCode)
           => StatusCode switch
           {
               100 => "Continue",
               101 => "Switching Protocols",
               102 => "Processing",
               200 => "OK",
               201 => "Created",
               202 => "Accepted",
               203 => "Non - Authoritative Information ",
               204 => "No Content",
               205 => "Reset Content ",
               206 => "Partial Content ",
               207 => "Multi - Status ",
               208 => "Already Reported ",
               226 => "IM Used ",
               300 => "Multiple Choices ",
               301 => "Moved Permanently ",
               302 => "Found ",
               303 => "See Other ",
               304 => "Not Modified ",
               305 => "Use Proxy ",
               306 => "Unused) ",
               307 => "Temporary Redirect ",
               308 => "Permanent Redirect ",
               400 => "Bad Request ",
               _ => "Unknown StatusCode"
           };

    }
}
