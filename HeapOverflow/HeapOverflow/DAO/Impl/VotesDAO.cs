﻿using HeapOverflow.Config;
using HeapOverflow.DAO.Inter;
using HeapOverflow.Entity;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace HeapOverflow.DAO.Impl
{
	public class VotesDAO : IVotesDAO
	{
		private readonly Logger _log = new Logger("VotesDAO");

		private Configuration config;
		private MySqlConnection con;

		private IUserLoginDAO loginDAO;
		private IPostDAO postDAO;

		public VotesDAO()
		{
			InitializeComponents();
		}

		private void InitializeComponents()
		{
			config = Configuration.GetConfig();
			con = new MySqlConnection(config.GetConnection().GenerateString());
			con.Open();

			loginDAO = Context.GetUserLoginDAO();
			postDAO = Context.GetPostDAO();
		}

		public Votes AddVote(Votes vote)
		{
			try
			{
				string query = "insert into vote (user, post, vote) values (@user, @post, @vote); select LAST_INSERT_ID()";

				using (MySqlCommand cmd = new MySqlCommand(query, con))
				{
					cmd.Parameters.AddWithValue("@user", vote.User.Id);
					cmd.Parameters.AddWithValue("@post", vote.Post.Id);
					cmd.Parameters.AddWithValue("@vote", vote.Vote);

					int id = Convert.ToInt32(cmd.ExecuteScalar());
					if (id > 0)
					{
						vote.Id = id;
						return vote;
					}
					else
						throw new Exception("Error occured while new Vote inserting!");
				}
			}
			catch (Exception ex)
			{
				_log.Log(ex.Message + "\r\n" + ex.StackTrace);
				return null;
			}
		}

		public Votes GetVote(UserLogin login, Post post)
		{
			try
			{
				string query = "select * from vote where user = @user and post = @post";

				using (MySqlCommand cmd = new MySqlCommand(query, con))
				{
					cmd.Parameters.AddWithValue("@user", login.Id);
					cmd.Parameters.AddWithValue("@post", post.Id);

					var mdr = cmd.ExecuteReader();
					Votes vote = null;
					if (mdr.Read())
					{
						vote = new Votes();
						FillVotesWithMDR(vote, mdr);
					}
					return vote;
				}
			}
			catch (Exception ex)
			{
				_log.Log(ex.Message + "\r\n" + ex.StackTrace);
				return null;
			}
		}

		private void FillVotesWithMDR(Votes vote, MySqlDataReader mdr)
		{
			vote.Id = Convert.ToInt32(mdr.GetString(mdr.GetOrdinal("id")));
			vote.Vote = Convert.ToInt32(mdr.GetString(mdr.GetOrdinal("vote")));

			var userId = Convert.ToInt32(mdr.GetString(mdr.GetOrdinal("user")));
			vote.User = loginDAO.GetUserLoginById(userId);

			var postId = Convert.ToInt32(mdr.GetString(mdr.GetOrdinal("post")));
			vote.Post = postDAO.GetPostById(postId);
		}

		public int GetLikesCountByPost(Post post)
		{
			try
			{
				string query = "select count(id) from vote where post = @post and vote = 1";

				using (MySqlCommand cmd = new MySqlCommand(query, con))
				{
					cmd.Parameters.AddWithValue("@post", post.Id);

					var vote = Convert.ToInt32(cmd.ExecuteScalar());
					return vote;
				}
			}
			catch (Exception ex)
			{
				_log.Log(ex.Message + "\r\n" + ex.StackTrace);
				return 0;
			}
		}

		public int GetDislikesCountByPost(Post post)
		{
			try
			{
				string query = "select count(id) from vote where post = @post and vote = 0";

				using (MySqlCommand cmd = new MySqlCommand(query, con))
				{
					cmd.Parameters.AddWithValue("@post", post.Id);

					var vote = Convert.ToInt32(cmd.ExecuteScalar());
					return vote;
				}
			}
			catch (Exception ex)
			{
				_log.Log(ex.Message + "\r\n" + ex.StackTrace);
				return 0;
			}
		}

		public void RemoveVote(int voteId)
		{
			try
			{
				string query = "delete from vote where id = @id";

				using (MySqlCommand cmd = new MySqlCommand(query, con))
				{
					cmd.Parameters.AddWithValue("@id", voteId);

					cmd.ExecuteNonQuery();
				}
			}
			catch (Exception ex)
			{
				_log.Log(ex.Message + "\r\n" + ex.StackTrace);
			}
		}

		public Votes UpdateVote(Votes vote)
		{
			try
			{
				string query = "update vote set vote = @vote where id = @id";

				using (MySqlCommand cmd = new MySqlCommand(query, con))
				{
					cmd.Parameters.AddWithValue("@vote", vote.Vote);
					cmd.Parameters.AddWithValue("@id", vote.Id);

					cmd.ExecuteNonQuery();
					return vote;
				}
			}
			catch (Exception ex)
			{
				_log.Log(ex.Message + "\r\n" + ex.StackTrace);
				return null;
			}
		}

		public void RemoveVotesByPost(int postId)
		{
			try
			{
				string query = "delete from vote where post = @post";

				using (MySqlCommand cmd = new MySqlCommand(query, con))
				{
					cmd.Parameters.AddWithValue("@post", postId);

					cmd.ExecuteNonQuery();
				}
			}
			catch (Exception ex)
			{
				_log.Log(ex.Message + "\r\n" + ex.StackTrace);
			}
		}
	}
}