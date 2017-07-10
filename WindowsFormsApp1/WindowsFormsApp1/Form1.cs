using DevExpress.XtraTreeList.Nodes;
using System.Data;
using System.Windows.Forms;
using MySql.Data.MySqlClient;
namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        DataTable dt = new DataTable();
        public Form1()
        {
            InitializeComponent();

            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("ParentID", typeof(int));
            dt.Columns.Add("Name", typeof(string));
            dt.Columns.Add("Number", typeof(string));
            dt.Columns.Add("W/M", typeof(string));

            treeList1.KeyFieldName = "ID";
            treeList1.ParentFieldName = "ParentID";
            treeList1.DataSource = dt;

            dt.Rows.Add(1, 0, "山东", 1);
            dt.Rows.Add(12, 0, "西安", 12);
        }
        #region  建立MySql数据库连接
        /// <summary>
        /// 建立数据库连接.
        /// </summary>
        /// <returns>返回MySqlConnection对象</returns>
        public MySqlConnection getmysqlcon()
        {
            string M_str_sqlcon = "server=localhost;user=root;database=world;port=3306;password=Gk280014"; //根据自己的设置
            MySqlConnection myCon = new MySqlConnection(M_str_sqlcon);
            return myCon;
        }
        #endregion
        #region  执行MySqlCommand命令
        /// <summary>
        /// 执行MySqlCommand
        /// </summary>
        /// <param name="M_str_sqlstr">SQL语句</param>
        public void getmysqlcom(string M_str_sqlstr)
        {
            MySqlConnection mysqlcon = this.getmysqlcon();
            mysqlcon.Open();
            MySqlCommand mysqlcom = new MySqlCommand(M_str_sqlstr, mysqlcon);
            mysqlcom.ExecuteNonQuery();
            mysqlcom.Dispose();
            mysqlcon.Close();
            mysqlcon.Dispose();
        }
        #endregion
        #region  创建MySqlDataReader对象
        /// <summary>
        /// 创建一个MySqlDataReader对象
        /// </summary>
        /// <param name="M_str_sqlstr">SQL语句</param>
        /// <returns>返回MySqlDataReader对象</returns>
        public MySqlDataReader getmysqlread(string M_str_sqlstr)
        {
            MySqlConnection mysqlcon = this.getmysqlcon();
            MySqlCommand mysqlcom = new MySqlCommand(M_str_sqlstr, mysqlcon);
            mysqlcon.Open();
            MySqlDataReader mysqlread = mysqlcom.ExecuteReader(CommandBehavior.CloseConnection);
            return mysqlread;
        }
        #endregion
        private void button1_Click(object sender, System.EventArgs e)
        {

            dt.Rows.Add(2, 1, "济南", 1);//添加数据
            dt.Rows.Add(1, 2, "济南", 1);
            dt.Rows.Add(3, 1, "泰安", 1);
            dt.Rows.Add(4, 1, "济宁", 1);
            dt.Rows.Add(5, 1, "菏泽", 1);

            dt.Rows.Add(1, 12, "济南", 2);
            dt.Rows.Add(2, 12, "泰安", 2);
            dt.Rows.Add(3, 12, "济宁", 2);
            dt.Rows.Add(4, 12, "菏泽", 2);
        }
        TreeListNode deletenode;
        private void button2_Click(object sender, System.EventArgs e)
        {
            treeList1.DeleteNode(treeList1.Nodes[0]);//删除父节点
        }

        private void treeList1_AfterFocusNode(object sender, DevExpress.XtraTreeList.NodeEventArgs e)
        {
            deletenode = e.Node; //获取最后的ID
        }

        private void button3_Click(object sender, System.EventArgs e)
        {
            treeList1.Nodes[0].Nodes[0].SetValue(0, "1111111");//改值
        }

        private void button4_Click(object sender, System.EventArgs e)
        {
            treeList1.DeleteNode(deletenode);//删除选中节点
        }

        private void treeList1_GetSelectImage(object sender, DevExpress.XtraTreeList.GetSelectImageEventArgs e)
        {
            if (e.Node == null) return;
            TreeListNode node = e.Node;

            int ID = (int)node.GetValue("ID");
            if (ID == 1)
                e.NodeImageIndex = 0;
            else
                e.NodeImageIndex = 2;
        }

        private void button5_Click(object sender, System.EventArgs e)
        {
            getmysqlcom("insert into blog(ID,name) values('2','abc')");
        }

        private void button6_Click(object sender, System.EventArgs e)
        {
            int i = 0;
            MySqlDataReader mysqlread = getmysqlread("select * from blog");
            while (mysqlread.Read())
            {
                treeList1.Nodes[0].Nodes[i].SetValue(0, mysqlread.GetString(1));
                i++;
            }

        }
    }
}
