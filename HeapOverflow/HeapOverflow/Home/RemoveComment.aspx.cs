﻿using HeapOverflow.DAO.Inter;
using HeapOverflow.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HeapOverflow.Home
{
    public partial class RemoveComment : System.Web.UI.Page
    {
        private IUserLoginDAO loginDAO = Config.Context.GetUserLoginDAO();
        private ICommentDAO commentDAO = Config.Context.GetCommentDAO();
        private IPostDAO postDAO = Config.Context.GetPostDAO();

        private UserLogin login;
        private int commentId;
        private int postId;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["user"] == null)
                Response.Redirect("Index.aspx");
            else
                DefineParameters();
        }

        private void DefineParameters ()
        {
            int.TryParse(Request.Params["commentId"], out commentId);
            int.TryParse(Request.Params["postId"], out postId);

            var parse = int.TryParse(Session["user"].ToString(), out int loginId);
            if (parse)
            {
                login = loginDAO.GetUserLoginById(loginId);
                if (login == null || (postDAO.GetPostById(postId).User.Id != login.Id && login.Role.Name.Equals("USER") && 
					commentDAO.GetCommentById(commentId).User.Id != login.Id))
                    Response.Redirect("Index.aspx");
            }
        }

        protected void btn_remove_Click(object sender, EventArgs e)
        {
            commentDAO.RemoveComment(commentId);
            Response.Redirect("PostDetail.aspx?id=" + postId);
        }
    }
}