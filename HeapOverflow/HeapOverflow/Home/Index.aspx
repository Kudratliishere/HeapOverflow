﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Index.aspx.cs" Inherits="HeapOverflow.Home.Index" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
	<title>HeapOverflow</title>
	<link rel="stylesheet" href="../Resources/Css/Index.css" />
	<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />
	<script src="../Resources/Js/Index.js"></script>
</head>
<body>
	<form id="index" runat="server">
		<header>
			<div class="navbar">
				<nav class="navigation hide" id="navigation">
					<ul class="nav-list">
						<span class="close-icon" onclick="showIconBar()"><i class="fa fa-close"></i></span>
						<li class="nav-item">
							<asp:Button ID="btn_home" runat="server" Text="Home" OnClick="btn_home_Click" />
						</li>
						<li class="nav-item">
							<asp:Button ID="btn_account" runat="server" Text="Account" OnClick="btn_account_Click" />
						</li>
					</ul>
				</nav>
				<a href="#" class="bar-icon" id="iconBar"><i class="fa fa-bars" onclick="hideIconBar()"></i></a>
				<a href="Index.aspx" style="text-decoration: none; color: white;">
					<div class="brand">Heap Overflow</div>
				</a>
				<div class="account">
					<asp:Button ID="btn_login" runat="server" Text="Login" CssClass="login" OnClick="btn_login_Click" />
					<asp:Button ID="btn_register" runat="server" Text="Register" CssClass="register" OnClick="btn_register_Click" />
					<asp:Button ID="btn_logout" runat="server" Text="Logout" CssClass="logout" OnClick="btn_logout_Click" />
				</div>
			</div>

			<!--search box-->
			<div class="search-box">
				<div>
					<asp:DropDownList ID="ddl_filter" runat="server">
						<asp:ListItem Selected="True">Everything</asp:ListItem>
						<asp:ListItem>Title</asp:ListItem>
						<asp:ListItem>Topic</asp:ListItem>
					</asp:DropDownList>
					<asp:TextBox ID="tb_search" runat="server" placeholder="search..."></asp:TextBox>
					<asp:Button ID="btn_search" runat="server" Text="Search" OnClick="btn_search_Click" />
				</div>
			</div>
		</header>
		<div class="container">
			<div class="subforum">
				<asp:Button ID="btn_new" runat="server" Text="Create new" OnClick="btn_new_Click" />
				<div class="posts-table">
					<div class="table-head">
						<div class="status">Status</div>
						<div class="subjects">Subjects</div>
						<div class="replies">Replies/Likes</div>
					</div>
					<asp:PlaceHolder ID="ph_table_row" runat="server"></asp:PlaceHolder>
				</div>
			</div>
		</div>

		<div class="pagination">
			pages:
			<asp:PlaceHolder ID="ph_pagination" runat="server"></asp:PlaceHolder>
		</div>

		<div class="forum-info">
			<div class="chart">
				Heap Overflow - Stats &nbsp;<i class="fa fa-bar-chart"></i>
			</div>
			<div>
				<span><u>5,359</u> Posts in <u>1,246</u> Topics by <u>45,215</u> Users</span>
			</div>
		</div>

		<footer>
			<span>&copy; &nbsp;Dunay Gudratli | All Rights Reserved.</span>
		</footer>
	</form>
</body>
</html>
