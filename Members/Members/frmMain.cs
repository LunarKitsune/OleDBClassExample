using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.OleDb;

namespace Members
{
    public partial class frmMain : Form
    {
        DataReader connStringGraber = new DataReader();

        OleDbDataAdapter memberDataAdapter;
        DataSet memberDataSet;
        OleDbCommandBuilder dbCommandBuilder;
        OleDbConnection dbConnection;

        string connection;
        string sql;


        #region Main Form Components
        public frmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {

        }

        #endregion Main Form Components


        #region form Buttons
        private void BtnLoad_Click(object sender, EventArgs e)
        {
            string[] connectionParts = connStringGraber.readConnectionStrings();

            connection = "Provider=Microsoft.ACE.OLEDB.12.0;" +
                    "Data Source=member.accdb";

            dbConnection = new OleDbConnection(connection);

            sql = "Select * from memberTable order by LastName ASC, FirstName ASC;";

            //provides the connection and the command for the connection to grab from, and and what from
            OleDbCommand dbCommand = new OleDbCommand();
            dbCommand.CommandText = sql;
            dbCommand.Connection = dbConnection;

            //dataAdapter helps read the database
            memberDataAdapter = new OleDbDataAdapter();
            memberDataAdapter.SelectCommand = dbCommand;

            //*instantiates the data set which will hold the table in memory
            //*gets the data adapter to fill the memebr dataset based on the command
            //given
            memberDataSet = new DataSet();
            memberDataAdapter.Fill(memberDataSet, "memberTable");

            //Datagrid view gets the assigned data source and the "Data member" name
            dgvMembers.DataSource = memberDataSet;
            dgvMembers.DataMember = "memberTable";
        }

        private void BtnUpdate_Click(object sender, EventArgs e)
        {
            try
            {
                dbCommandBuilder = new OleDbCommandBuilder(memberDataAdapter);
                memberDataAdapter.Update(memberDataSet, "memberTable");
                MessageBox.Show("Members Updated", "Members");
            }
            catch(SystemException ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void BtnDelete_Click(object sender, EventArgs e)
        {
            DialogResult dbResult = MessageBox.Show("Delete Record", "Record", MessageBoxButtons.YesNo);

            if(dbResult == DialogResult.Yes)
            {
                dgvMembers.Rows.RemoveAt(dgvMembers.CurrentRow.Index);
                BtnUpdate_Click(sender, e);
            }
        }

        private void BtnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        #endregion formButtons
    }
}
