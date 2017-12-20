using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;

namespace EP
{
    class DataAccess
    {
        
        public int addMusic(int m_id, String m_name, String m_path, int r_id, int l_id, String b_name, String g_name)
        {

            OracleConnection conn;
            OracleCommand comm;
            
            conn = new MyConClass().getConnection();
            comm = new OracleCommand();
            comm.Connection = conn;
            
            String result = getInfo("g_id", "genre", "g_name", g_name);
            int g_id;

            if(result.Equals("error"))
            {
                g_id = 0;
            }
            else
            {
                g_id = Int32.Parse(result);
            }
            
            comm.CommandText = "root.add_music";
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.Add("r1", OracleDbType.Int32, 32);
            comm.Parameters["r1"].Direction = ParameterDirection.ReturnValue;

            comm.Parameters.Add("mid", OracleDbType.Int32, 32);
            comm.Parameters["mid"].Value = m_id;

            comm.Parameters.Add("mname", OracleDbType.Varchar2);
            comm.Parameters["mname"].Value = m_name;

            comm.Parameters.Add("mpath", OracleDbType.Varchar2);
            comm.Parameters["mpath"].Value = m_path;

            comm.Parameters.Add("rid", OracleDbType.Int32, 32);
            comm.Parameters["rid"].Value = r_id;

            comm.Parameters.Add("lid", OracleDbType.Int32, 32);
            comm.Parameters["lid"].Value = l_id;

            comm.Parameters.Add("bname", OracleDbType.Varchar2);
            comm.Parameters["bname"].Value = b_name;

            comm.Parameters.Add("gid", OracleDbType.Int32, 32);
            comm.Parameters["gid"].Value = g_id;
            
            comm.ExecuteNonQuery();
            String rval = comm.Parameters["r1"].Value.ToString();

            

            if (rval.Equals("-1"))
                return -1;
            else
                return 0;

        }

        public String getInfo(String what, String table, String condition, String conditionValue)
        {
            OracleConnection conn;
            OracleCommand comm;

            conn = new MyConClass().getConnection();
            comm = new OracleCommand();
            comm.Connection = conn;
            
            comm.CommandText = "select "+what+" from "+table+" where "+condition+" = '"+conditionValue+"' ";
            comm.CommandType = CommandType.Text;
            OracleDataReader read = comm.ExecuteReader();
            comm.CommandText = "select count(*) as c from "+table+" where "+condition+"='"+conditionValue+"'";
            OracleDataReader read1 = comm.ExecuteReader();
            read.Read();
            read1.Read();
            if(!read1["c"].ToString().Equals("0"))
            {
                return read[what].ToString();
            }
            else
            {
                return "error";
            }
        }

        public OracleDataReader getMusic()
        {
            OracleConnection conn;
            OracleCommand comm;
            conn = new MyConClass().getConnection();
            comm = new OracleCommand();
            comm.Connection = conn;
            comm.CommandText = "select m_id, m_name, m_path from music";
            comm.CommandType = CommandType.Text;
            OracleDataReader read = comm.ExecuteReader();
            return read;
        }
        public OracleDataReader getMusicByPlaylist(string p_name)
        {
            OracleConnection conn;
            OracleCommand comm;
            conn = new MyConClass().getConnection();
            comm = new OracleCommand();
            comm.Connection = conn;
            String p_id = getInfo("p_id", "playlists", "p_name", p_name);
            comm.CommandText = "select m_id, m_name, m_path from music where m_id in (select m_id from music2playlists where p_id=" + p_id + ")";
            comm.CommandType = CommandType.Text;
            OracleDataReader read = comm.ExecuteReader();
            return read;
        }
        public List<String> getPlaylistMusicOptions(String p_name)
        {
            OracleDataReader playlistMusic = getMusicByPlaylist(p_name);
            OracleDataReader allMusic = getMusic();
            List<String> result = new List<string>();
            
            
            while(allMusic.Read())
            {
                bool flag = false;
                while(playlistMusic.Read())
                {
                    if(playlistMusic["m_name"].ToString().Equals(allMusic["m_name"].ToString()))
                    {
                        flag = true;
                    }
                }
                if(!flag)
                {
                    result.Add(allMusic["m_name"].ToString());
                }
                playlistMusic = getMusicByPlaylist(p_name);
                
            }
            return result;
        }

        public void createPlaylist(String p_id, String p_name)
        {
            OracleConnection conn;
            OracleCommand comm;
            conn = new MyConClass().getConnection();
            comm = new OracleCommand();
            comm.Connection = conn;
            comm.CommandText = "insert into playlists values(" + p_id + ", '" + p_name + "')";
            comm.CommandType = CommandType.Text;
            comm.ExecuteNonQuery();
        }

        public OracleDataReader getPlaylists()
        {
            OracleConnection conn;
            OracleCommand comm;
            conn = new MyConClass().getConnection();
            comm = new OracleCommand();
            comm.Connection = conn;
            comm.CommandText = "select p_id, p_name from playlists";
            comm.CommandType = CommandType.Text;
            OracleDataReader read = comm.ExecuteReader();
            return read;
        }

        public void addRating(String r_id, String r_score, String r_description, String m_name)
        {
            OracleConnection conn;
            OracleCommand comm;
            conn = new MyConClass().getConnection();
            comm = new OracleCommand();
            /*comm.CommandText = "root.rating_add";
            comm.CommandType = CommandType.StoredProcedure;

            comm.Parameters.Add("r1", OracleDbType.Int32, 32);
            comm.Parameters["r1"].Direction = ParameterDirection.ReturnValue;

            comm.Parameters.Add("rid", OracleDbType.Int32, 32);
            comm.Parameters["rid"].Value = Int32.Parse(r_id);

            comm.Parameters.Add("rscore", OracleDbType.Int32, 32);
            comm.Parameters["rscore"].Value = Int32.Parse(r_score);

            comm.Parameters.Add("rdescription", OracleDbType.Varchar2);
            comm.Parameters["rdescription"].Value = r_description;

            comm.Parameters.Add("mname", OracleDbType.Varchar2);
            comm.Parameters["mname"].Value = m_name;

            comm.ExecuteNonQuery();*/
            comm.CommandText = "insert into rating values("+r_id+", +"+r_score+", '+"+r_description+"')";
            comm.CommandType = CommandType.Text;
            try
            {
                comm.ExecuteNonQuery();
            
            comm.CommandText = "update music set r_id = "+r_id+" where m_name = "+m_name;
            comm.CommandType = CommandType.Text;
            comm.ExecuteNonQuery();
            }
            catch (Exception e11)
            {

            }
        }

        public OracleDataReader getMoods()
        {
            OracleConnection conn;
            OracleCommand comm;
            conn = new MyConClass().getConnection();
            comm = new OracleCommand();
            comm.Connection = conn;
            comm.CommandText = "select mo_id, mo_name from moods";
            comm.CommandType = CommandType.Text;
            OracleDataReader read = comm.ExecuteReader();
            return read;
        }

        public OracleDataReader getMusicByMood(String mo_name)
        {
            OracleConnection conn;
            OracleCommand comm;
            conn = new MyConClass().getConnection();
            comm = new OracleCommand();
            comm.Connection = conn;
            String mo_id = getInfo("mo_id", "moods", "mo_name", mo_name);
            comm.CommandText = "select m_id, m_name, m_path from music where g_id in (select g_id from genre where mo_id=" + mo_id + ")";
            comm.CommandType = CommandType.Text;
            OracleDataReader read = comm.ExecuteReader();
            return read;
        }

        public int addToPlaylist(String m_name, String p_name)
        {
            OracleConnection conn;
            OracleCommand comm;
            conn = new MyConClass().getConnection();
            comm = new OracleCommand();
            String m_id = getInfo("m_id", "music", "m_name", m_name);
            String p_id = getInfo("p_id", "playlists", "p_name", p_name);
            if (m_id.Equals("error") || p_id.Equals("error"))
                return -1;
            else
            {
                comm.CommandText = "insert into music2playlists(m_id, p_id) values(" + m_id + "," + p_id + ")";
                comm.CommandType = CommandType.Text;
                comm.ExecuteNonQuery();
                return 0;
            }
         }

        public OracleDataReader getArtists()
        {
            OracleConnection conn;
            OracleCommand comm;
            conn = new MyConClass().getConnection();
            comm = new OracleCommand();
            comm.Connection = conn;
            comm.CommandText = "select * from artists";
            comm.CommandType = CommandType.Text;
            OracleDataReader read = comm.ExecuteReader();
            return read;
        }
        /*
        public void addLyrics(String l_id, String l_name, String l_path, String m_name)
        {
            comm.CommandText = "insert into lyrics values(" + l_id + ", '" + l_name + "','"+l_path+"')";
            comm.CommandType = CommandType.Text;
            comm.ExecuteNonQuery();
            comm.CommandText = "update music set l_id="+l_id+" where m_name='"+m_name+"'";
            comm.CommandType = CommandType.Text;
            comm.ExecuteNonQuery();
        }

        public void addBand(String b_id, String b_name, String s_date, String e_date)
        {
            comm.CommandText = "insert into bands values(" + b_id + ", '" + b_name + "','" + s_date + "','"+e_date+"')";
            comm.CommandType = CommandType.Text;
            comm.ExecuteNonQuery();
        }

        public void addArtist(String a_id, String a_name, String a_age, String a_phno, String b_name)
        {
            String b_id = getInfo("b_id", "bands", "b_name", b_name);
            comm.CommandText = "insert into artists values(" + a_id + ", '" + a_name + "'," + a_age + "," + a_phno + ","+b_id+")";
            comm.CommandType = CommandType.Text;
            comm.ExecuteNonQuery();
        }
        
        public OracleDataReader getMusicByBand(String b_name)
        {
            String b_id = getInfo("b_id", "bands", "b_name", b_name);
            comm.CommandText = "select m_id, m_name, m_path, b_id from music where b_id="+b_id;
            comm.CommandType = CommandType.Text;
            OracleDataReader read = comm.ExecuteReader();
            return read;
        }


        public OracleDataReader getLyrics(String m_name)
        {
            String m_id = getInfo("m_id", "music", "m_name", m_name);
            comm.CommandText = "select l_id, l_name, l_path from lyrics where l_id in (select l_id from music where m_id="+m_id+")";
            comm.CommandType = CommandType.Text;
            OracleDataReader read = comm.ExecuteReader();
            return read;
        }

        public OracleDataReader getRating(String m_name)
        {
            comm.CommandText = "select r_id, r_score, r_description from rating where r_id in (select r_id from music where m_name="+m_name+")";
            comm.CommandType = CommandType.Text;
            OracleDataReader read = comm.ExecuteReader();
            return read;
        }*/

        
    }
}
