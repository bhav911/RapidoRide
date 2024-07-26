﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BhavyaModhiya_490.SessionHelper
{
    public class UserSession
    {
        public static int UserID
        {
            get
            {
                return (int)(HttpContext.Current.Session["UserID"] == null ? 0 : HttpContext.Current.Session["UserID"]);
            }
            set
            {
                HttpContext.Current.Session["UserID"] = value;
            }
        }

        public static string Username
        {
            get
            {
                return (string)(HttpContext.Current.Session["Username"] == null ? 0 : HttpContext.Current.Session["Username"]);
            }
            set
            {
                HttpContext.Current.Session["Username"] = value;
            }
        }

        public static string UserRole
        {
            get
            {
                return (string)(HttpContext.Current.Session["UserRole"] == null ? 0 : HttpContext.Current.Session["UserRole"]);
            }
            set
            {
                HttpContext.Current.Session["UserRole"] = value;
            }
        }
    }
}