using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Domain.Entities;

namespace WebUI.Infrastructure
{
    public static class SessionManager //: ISession
    {
        public static void RegisterSession(string key, object obj)
        {
            System.Web.HttpContext.Current.Session[key] = obj;
        }

        public static void FreeSession(string key)
        {
            System.Web.HttpContext.Current.Session[key] = null;
        }


        public static bool CheckSession(string key)
        {

            if (System.Web.HttpContext.Current.Session[key] != null)
                return true;
            else
                return false;
        }


        public static object ReturnSessionObject(string key)
        {
            if (CheckSession(key))
                return System.Web.HttpContext.Current.Session[key];
            else
                return null;
        }

        public static bool CheckUserIsInRole(string Roles)
        {

            return (System.Web.HttpContext.Current.Session[
                System.Web.HttpContext.Current.Session.SessionID] != null) 
                ? ((Person)System.Web.HttpContext.Current.Session[
                System.Web.HttpContext.Current.Session.SessionID]).InRoles(Roles) 
                : false;//
            
        }
        public static Person GetPerson()
        {
            return (System.Web.HttpContext.Current.Session[
                System.Web.HttpContext.Current.Session.SessionID] != null)
                ? ((Person)System.Web.HttpContext.Current.Session[
                System.Web.HttpContext.Current.Session.SessionID])
                : null;
        }
    }
}