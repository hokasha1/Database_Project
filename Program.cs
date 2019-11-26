using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Diagnostics;

namespace Database_Project
{
    class Program
    {
        static void Main(string[] args)
        {
            CreateDatabases();
            while (true) { } // Holds program
        }

        private static void CreateDatabases()
        {
            try
            {
                SQLiteConnection.CreateFile("baseDatabase.sqlite");
                SetupDatabaseTables("baseDatabase.sqlite");

                SQLiteConnection.CreateFile("decomp1.sqlite");
                SetupDatabaseTables1("decomp1.sqlite");

                SQLiteConnection.CreateFile("decomp2.sqlite");
                SetupDatabaseTables2("decomp2.sqlite");

                TestInsert("baseDatabase.sqlite");
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error occured while creating databases.\n   Execption: {e}");
            }
        }

        private static void SetupDatabaseTables(string DatabaseFile)
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection($"Data Source={DatabaseFile};Version=3;");
            m_dbConnection.Open();
            string sql_querey;

            // Adds Venue Table
            sql_querey = "create table venue(venue_ID integer PRIMARY KEY AUTOINCREMENT,venue_Name text,Location text,chair_ID integer,event_ID integer,FOREIGN KEY(chair_ID) REFERENCES chairs,FOREIGN KEY(event_ID) REFERENCES event);";
            tryQuerey(sql_querey, m_dbConnection);
            // Adds Event Table
            sql_querey = "create table event (event_ID integer PRIMARY KEY AUTOINCREMENT, venue_ID integer, event_Title text, title_ID integer,FOREIGN KEY(title_ID) REFERENCES papers,FOREIGN KEY(venue_ID) REFERENCES venue); ";
            tryQuerey(sql_querey, m_dbConnection);
            // Adds Author Table
            sql_querey = "create table authors(author_ID integer PRIMARY KEY AUTOINCREMENT, a_name text, title_ID integer,FOREIGN KEY(title_ID) REFERENCES papers); ";
            tryQuerey(sql_querey, m_dbConnection);
            // Adds Papers Table
            sql_querey = "create table papers(title_ID integer PRIMARY KEY AUTOINCREMENT,paper_Title text,author_ID integer,event_ID integer, FOREIGN KEY(event_ID) REFERENCES event,FOREIGN Key(author_ID) REFERENCES authors);";
            tryQuerey(sql_querey, m_dbConnection);
            // Adds Reviewers Table
           sql_querey = "create table reviewers(reviewer_ID integer PRIMARY KEY AUTOINCREMENT, title_ID integer, chair_ID integer, r_Name text,FOREIGN KEY(title_ID) REFERENCES papers,FOREIGN Key(chair_ID) REFERENCES chairs); ";
            tryQuerey(sql_querey, m_dbConnection);
            // Adds Chairs Table
            sql_querey = "create table chairs(chair_ID integer PRIMARY KEY AUTOINCREMENT,chair_Name text,venue_ID integer,FOREIGN Key(venue_ID) REFERENCES venue); ";
            tryQuerey(sql_querey, m_dbConnection);

            m_dbConnection.Close();
        }
        private static void SetupDatabaseTables1(string DatabaseFile)
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection($"Data Source={DatabaseFile};Version=3;");
            m_dbConnection.Open();
            string sql_querey;

            // Adds Event Table (delete reference to foregin keys to entities not in decomp)
            sql_querey = "create table event (event_ID integer PRIMARY KEY AUTOINCREMENT, venue_ID integer, event_Title text, title_ID integer, FOREIGN KEY(title_ID) REFERENCES papers); ";
            tryQuerey(sql_querey, m_dbConnection);

            // Adds Author Table
               sql_querey = "create table authors(author_ID integer PRIMARY KEY AUTOINCREMENT, a_name text, title_ID integer,FOREIGN KEY(title_ID) REFERENCES papers); ";
              tryQuerey(sql_querey, m_dbConnection);
            // Adds Papers Table
              sql_querey = "create table papers(title_ID integer PRIMARY KEY AUTOINCREMENT,paper_Title text,author_ID integer,event_ID integer,FOREIGN Key(event_ID) REFERENCES event,FOREIGN Key(author_ID) REFERENCES authors);";
              tryQuerey(sql_querey, m_dbConnection);
            // Adds Reviewers Table
                sql_querey = "create table reviewers(reviewer_ID integer PRIMARY KEY AUTOINCREMENT, title_ID integer, chair_ID integer, r_Name text,FOREIGN KEY(title_ID) REFERENCES papers); ";
                tryQuerey(sql_querey, m_dbConnection);

            m_dbConnection.Close();
        }

        private static void SetupDatabaseTables2(string DatabaseFile)
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection($"Data Source={DatabaseFile};Version=3;");
            m_dbConnection.Open();
            string sql_querey;

            // Adds Venue Table
            sql_querey = "create table venue(venue_ID integer PRIMARY KEY AUTOINCREMENT,venue_Name text,Location text,chair_ID integer,event_ID integer,FOREIGN KEY(chair_ID) REFERENCES chairs,FOREIGN KEY(event_ID) REFERENCES event);";
            tryQuerey(sql_querey, m_dbConnection);
            // Adds Event Table (delete reference to foregin keys to entities not in decomp)
            sql_querey = "create table event (event_ID integer PRIMARY KEY AUTOINCREMENT, venue_ID integer, event_Title text, title_ID integer, FOREIGN KEY(venue_ID) REFERENCES venue); ";
            tryQuerey(sql_querey, m_dbConnection);

            // Adds Chairs Table
            sql_querey = "create table chairs(chair_ID integer PRIMARY KEY AUTOINCREMENT,chair_Name text,venue_ID integer,FOREIGN Key(venue_ID) REFERENCES venue); ";
            tryQuerey(sql_querey, m_dbConnection);

            m_dbConnection.Close();
        }

        private static void FillTables(string DatabaseFile)
        {
            List<venue> m_venues = new List<venue>();
            List<events> m_events = new List<events>();
         //   List<papers> m_papers = new List<papers>();
         //   List<authors> m_authors = new List<authors>();
            List<chairs> m_chairs = new List<chairs>();
         //   List<reviewers> m_reviewers = new List<reviewers>();
        }

        private static void TestInsert(string DatabaseFile)
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection($"Data Source={DatabaseFile};Version=3;");
            m_dbConnection.Open();
            string sql_querey;

            for(int i = 0; i < 100; i++)
            {
                sql_querey = $"INSERT INTO venue (venue_ID,venue_Name,Location,chair_ID,event_ID) VALUES ({i},'venue_Name: {i}','Location: {i}',{i},{i});";
                tryQuerey(sql_querey,m_dbConnection);

                sql_querey = $"INSERT INTO event (event_ID,venue_ID,event_Title,title_ID) VALUES({i}, {i}, 'Event_Title: {i}',{i});";
                tryQuerey(sql_querey, m_dbConnection);

                sql_querey = $"INSERT INTO chairs (chair_ID,chair_Name,venue_ID) VALUES({i}, 'chair_Name: {i}', {i}); ";
                tryQuerey(sql_querey, m_dbConnection);

                sql_querey = $"INSERT INTO papers (title_ID,paper_Title,author_ID,event_ID) VALUES({i}, 'paper_Title: {i}', {i},{i}); ";
                tryQuerey(sql_querey, m_dbConnection);

                sql_querey = $"INSERT INTO reviewers (reviewer_ID, title_ID, chair_ID, r_Name) VALUES({i}, {i}, {i},'reviewer_Name:{i}');";
                tryQuerey(sql_querey, m_dbConnection);

                sql_querey = $"INSERT INTO authors (author_ID,a_name,title_ID) VALUES({i}, 'author_Name:{i}', {i}); ";
                tryQuerey(sql_querey, m_dbConnection);
            }
            m_dbConnection.Close();

           TestSelect1("decomp1.sqlite");
           TestSelect2("decomp2.sqlite");
            
        }
        private static void TestSelect1(string DatabaseFile)
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection($"Data Source=baseDatabase.sqlite;Version=3;");
            m_dbConnection.Open();

            //************INSERT FROM OG TO PAPERS****************
            string sql_querey = "select * from papers;";
            var command = new SQLiteCommand(sql_querey, m_dbConnection);
            var dt = command.ExecuteReader();

            var m_dbConnection2 = new SQLiteConnection($"Data Source={DatabaseFile};Version=3;");
            m_dbConnection2.Open();
            // For each returned row in query
            while (dt.Read())
            {
                sql_querey = $"INSERT INTO papers (title_ID,paper_Title,author_ID,event_ID) VALUES({dt[0]}, '{dt[1]}','{dt[2]}',{dt[3]});";
                command = new SQLiteCommand(sql_querey, m_dbConnection2);
                command.ExecuteNonQuery();
            }

            //************INSERT FROM OG TO AUTHORS****************
            string sql_querey1 = "select * from authors;";
            var command1 = new SQLiteCommand(sql_querey1, m_dbConnection);
            var dt1 = command1.ExecuteReader();

            // For each returned row in query
            while (dt1.Read())
            {

                sql_querey1 = $"INSERT INTO authors (author_ID,a_name,title_ID) VALUES ({dt1[0]}, '{dt1[1]}',{dt1[2]});";
                command1 = new SQLiteCommand(sql_querey1, m_dbConnection2);
                command1.ExecuteNonQuery();

            }

            //************INSERT FROM OG TO EVENT****************
            string sql_querey2 = "select * from event;";
            var command2 = new SQLiteCommand(sql_querey2, m_dbConnection);
            var dt2 = command2.ExecuteReader();

            // For each returned row in query
            while (dt2.Read())
            {

                sql_querey2 = $"INSERT INTO event (event_ID,venue_ID,event_Title,title_ID) VALUES ({dt2[0]}, {dt2[1]},'{dt2[2]}',{dt2[3]});";
                command2 = new SQLiteCommand(sql_querey2, m_dbConnection2);
                command2.ExecuteNonQuery();
            }

            //************INSERT FROM OG TO REVIEWERS****************
            string sql_querey3 = "select * from reviewers;";
            var command3 = new SQLiteCommand(sql_querey3, m_dbConnection);
            var dt3 = command3.ExecuteReader();

            // For each returned row in query
            while (dt3.Read())
            {

                sql_querey3 = $"INSERT INTO reviewers (reviewer_ID, title_ID, chair_ID, r_Name) VALUES({dt3[0]}, {dt3[1]},{dt3[2]},'{dt3[3]}');";
                command3 = new SQLiteCommand(sql_querey3, m_dbConnection2);
                command3.ExecuteNonQuery();
            }

            m_dbConnection2.Close();
            m_dbConnection.Close();

        }
        private static void TestSelect2(string DatabaseFile)
        {
            SQLiteConnection m_dbConnection = new SQLiteConnection($"Data Source=baseDatabase.sqlite;Version=3;");
            m_dbConnection.Open();

            //************INSERT FROM OG TO VENUE****************
            string sql_querey = "select * from venue;";
            var command = new SQLiteCommand(sql_querey, m_dbConnection);
            var dt = command.ExecuteReader();

            var m_dbConnection2 = new SQLiteConnection($"Data Source={DatabaseFile};Version=3;");
            m_dbConnection2.Open();
            // For each returned row in query
            while (dt.Read())
            {                
                sql_querey = $"insert into venue (venue_ID, venue_Name,location,chair_ID,event_ID) values ({dt[0]}, '{dt[1]}','{dt[2]}',{dt[3]},{dt[4]});";
                command = new SQLiteCommand(sql_querey, m_dbConnection2);
                command.ExecuteNonQuery();
                
            }

            //************INSERT FROM OG TO CHAIRS****************
            string sql_querey1 = "select * from chairs;";
            var command1 = new SQLiteCommand(sql_querey1, m_dbConnection);
            var dt1 = command1.ExecuteReader();
        
            // For each returned row in query
            while (dt1.Read())
            {

                sql_querey1 = $"INSERT INTO chairs (chair_ID,chair_Name,venue_ID) VALUES ({dt1[0]}, '{dt1[1]}',{dt1[2]});";
                command1 = new SQLiteCommand(sql_querey1, m_dbConnection2);
                command1.ExecuteNonQuery();

            }

            //************INSERT FROM OG TO EVENT****************
            string sql_querey2 = "select * from event;";
            var command2 = new SQLiteCommand(sql_querey2, m_dbConnection);
            var dt2 = command2.ExecuteReader();

            // For each returned row in query
            while (dt2.Read())
            {
                sql_querey2 = $"INSERT INTO event (event_ID,venue_ID,event_Title,title_ID) VALUES ({dt2[0]}, {dt2[1]},'{dt2[2]}',{dt2[3]});";
                command2 = new SQLiteCommand(sql_querey2, m_dbConnection2);
                command2.ExecuteNonQuery();
            }

            m_dbConnection2.Close();
            m_dbConnection.Close();

        }

        private static void tryQuerey(string sql_querey, SQLiteConnection dbConn)
        {
            try
            {
                var command = new SQLiteCommand(sql_querey, dbConn);
                command.ExecuteNonQuery();
            }
            catch (Exception e)
            {
                Console.WriteLine($"Querey {sql_querey} could not be run.\n   Execption: {e}");
            }
        }
    }

    class events{
        public int event_ID { get; set; }
        public int venue_ID { get; set; }
        public string event_Title { get; set;}
        public int title_ID { get; set; }

        public events(int e, int v, string et,int t)
        {
            event_ID = e;
            venue_ID = v;
            event_Title = et;
            title_ID = t;
        }
    }

    class papers {
        public int title_ID { get; set; }
        public string paper_Title { get; set; }
        public int author_ID { get; set; }
        public int event_ID { get; set; }

        public papers(int t,string pt, int a,int e)
        {
            title_ID = t;
            paper_Title = pt;
            author_ID = a;
            event_ID = e;
        }
    }

    class authors {
        public int author_ID { get; set; }
        public string a_name { get; set; }
        public int title_ID { get; set; }

        public authors(int a, string an,int t)
        {
            author_ID = a;
            a_name = an;
            title_ID = t;
        }
    }

    class chairs {
        public int chair_ID { get; set; }
        public string chair_Name { get; set; }
        public int venue_ID { get; set; }

        public chairs(int c, string cn, int v)
        {
            chair_ID = c;
            chair_Name = cn;
            venue_ID = v;
        }
    }

    class reviewers {
        public int reviewer_ID { get; set; }
        public int title_ID { get; set; }
        public int chair_ID { get; set; }
        public string r_name { get; set; }
        public reviewers(int r, int t, int c,string rn)
        {
            reviewer_ID = r;
            title_ID = t;
            chair_ID = c;
            r_name = rn;
        }
    }

    class venue
    {
        public int venue_ID { get; set; }
        public string venue_name { get; set; }
        public string location { get; set; }
        public int chair_ID { get; set; }
        public int event_ID { get; set; }

        public venue(int v, string vn, string l, int c, int e)
        {
            venue_ID = v;
            venue_name = vn;
            location = l;
            chair_ID = c;
            event_ID = e;
        }
    }
}
