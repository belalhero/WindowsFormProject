using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;
using System.Configuration;
using MyHotelBookingProject.Reports;




namespace MyHotelBookingProject
{
    public partial class Form1 : Form
    {

        string cs = ConfigurationManager.ConnectionStrings["DbCon"].ConnectionString;
        SqlCommand cmd = new SqlCommand();
        public Form1()
        {
            InitializeComponent();
            panSelect.Height = btnHome.Height;
            panSelect.Top = btnHome.Top;

            panHome.Visible = true;
            panRoom.Visible = true;
            panCheckIn.Visible = true;
            panCheckOut.Visible = true;
            panReservation.Visible = true;
            panReport.Visible = true;
            panArchive.Visible = true;
            panCatering.Visible = true;
            panLogIn.Visible = true;
        }

        private void btnHome_Click(object sender, EventArgs e)
        {
            panSelect.Height = btnHome.Height;
            panSelect.Top = btnHome.Top;

            panHome.Visible = true;
            panRoom.Visible = false;
            panCheckIn.Visible = false;
            panCheckOut.Visible = false;
            panReservation.Visible = false;
            panReport.Visible = false;
            panArchive.Visible = false;
            panCatering.Visible = false;
        }

        private void btnRoom_Click(object sender, EventArgs e)
        {
            panSelect.Height = btnRoom.Height;
            panSelect.Top = btnRoom.Top;

            panHome.Visible = true;
            panRoom.Visible = true;
            panCheckIn.Visible = false;
            panCheckOut.Visible = false;
            panReservation.Visible = false;
            panReport.Visible = false;
            panArchive.Visible = false;
            panLogIn.Visible = false;
            panCatering.Visible = false;

            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                string showQry = "SELECT * FROM CoupleRoom";
                SqlCommand cmd = new SqlCommand(showQry, con);
                cmd = new SqlCommand(showQry, con);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dgViewCoupleRoom.DataSource = dt.DefaultView;

                string showDelux = "SELECT * FROM DeluxRoom";
                SqlCommand cmdDelux = new SqlCommand(showDelux, con);
                cmdDelux = new SqlCommand(showDelux, con);
                DataTable delux = new DataTable();
                SqlDataAdapter dlx = new SqlDataAdapter(cmdDelux);
                dlx.Fill(delux);
                dgViewDeluxRoom.DataSource = delux.DefaultView;
                con.Close();
            }

            Condition();
            CountRoom();
            DeluxInfo();
        }

        private void btnCheckIn_Click(object sender, EventArgs e)
        {
            panSelect.Height = btnCheckIn.Height;
            panSelect.Top = btnCheckIn.Top;

            panHome.Visible = true;
            panRoom.Visible = true;
            panCheckIn.Visible = true;
            panCheckOut.Visible = false;
            panReservation.Visible = false;
            panReport.Visible = false;
            panArchive.Visible = false;
            panCatering.Visible = false;

            // for comboBox
            Fillcombo();
        }

        private void btnCheckOut_Click(object sender, EventArgs e)
        {
            panSelect.Height = btnCheckOut.Height;
            panSelect.Top = btnCheckOut.Top;

            panHome.Visible = true;
            panRoom.Visible = true;
            panCheckIn.Visible = true;
            panCheckOut.Visible = true;
            panReservation.Visible = false;
            panReport.Visible = false;
            panArchive.Visible = false;
            panCatering.Visible = false;


            // For checkout search
            SqlConnection con = new SqlConnection(cs);
            SqlCommand cmd = new SqlCommand("SELECT Name, RoomNo FROM CheckIn Where IsActive = 1", con);
            con.Open();
            SqlDataAdapter dr = new SqlDataAdapter(cmd);
            AutoCompleteStringCollection myCollection = new AutoCompleteStringCollection();
            DataTable dt = new DataTable();
            dr.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    myCollection.Add(dt.Rows[i]["Name"].ToString());
                }
            }
            txtAutoSearchCheckOut.AutoCompleteCustomSource = myCollection;


            // For Room
            SqlDataAdapter dr2 = new SqlDataAdapter(cmd);
            AutoCompleteStringCollection myCollection2 = new AutoCompleteStringCollection();
            DataTable dt2 = new DataTable();
            dr2.Fill(dt2);
            if (dt2.Rows.Count > 0)
            {
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    myCollection2.Add(dt2.Rows[i]["RoomNo"].ToString());
                }
            }
            txtAutoSearchCheckOutRoom.AutoCompleteCustomSource = myCollection2;
            con.Close();

        }
    

    private void btnReservation_Click(object sender, EventArgs e)
        {
            panSelect.Height = btnReservation.Height;
            panSelect.Top = btnReservation.Top;

            panHome.Visible = true;
            panRoom.Visible = true;
            panCheckIn.Visible = true;
            panCheckOut.Visible = true;
            panReservation.Visible = true;
            panReport.Visible = false;
            panArchive.Visible = false;
            panCatering.Visible = false;

            FillcomboReservationAvailable();
            FillcomboPending();
            FillcomboPendingDelux();
        }

        public void DropDownAvailableReservationRoomType()
        {
            if (cmbBoxRoomType.Text == "Couple Room")
            {
                SqlConnection con = new SqlConnection(cs);
                con.Open();
                SqlDataAdapter daRoomNo = new SqlDataAdapter("SELECT RoomNo FROM CoupleRoom WHERE S = 1", con);
                DataTable dtRoomNo = new DataTable();
                daRoomNo.Fill(dtRoomNo);
                cmbReservationRoomNo.DataSource = dtRoomNo;
                cmbReservationRoomNo.DisplayMember = "RoomNo";
                con.Close();
            }
            else if (cmbBoxRoomType.Text == "Delux Suit")
            {
                SqlConnection con = new SqlConnection(cs);
                con.Open();
                SqlDataAdapter daRoomNo = new SqlDataAdapter("SELECT RoomNo FROM DeluxRoom WHERE S = 1", con);
                DataTable dtRoomNo = new DataTable();
                daRoomNo.Fill(dtRoomNo);
                cmbReservationRoomNo.DataSource = dtRoomNo;
                cmbReservationRoomNo.DisplayMember = "RoomNo";
                con.Close();
            }
            else
            {
                SqlConnection con = new SqlConnection(cs);
                con.Open();
                SqlDataAdapter daRoomNo = new SqlDataAdapter("SELECT RoomNo FROM NoRoom", con);
                DataTable dtRoomNo = new DataTable();
                daRoomNo.Fill(dtRoomNo);
                cmbReservationRoomNo.DataSource = dtRoomNo;
                cmbReservationRoomNo.DisplayMember = "RoomNo";
                con.Close();
            }
        }

        private void btnCancelReservation_Click(object sender, EventArgs e)
        {
            //panSelect.Height = btnCancelReservation.Height;
            //panSelect.Top = btnCancelReservation.Top;
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            panSelect.Height = btnReport.Height;
            panSelect.Top = btnReport.Top;
            this.menuStrip1.ForeColor = Color.White;
            this.menuStrip1.BackColor = Color.Black;

            panHome.Visible = true;
            panRoom.Visible = true;
            panCheckIn.Visible = true;
            panCheckOut.Visible = true;
            panReservation.Visible = true;
            panReport.Visible = true;
            panArchive.Visible = false;
            panCatering.Visible = false;


            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string gdView = "Select * from Calculation";
            cmd = new SqlCommand(gdView, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt.DefaultView;
            con.Close();

        }

        private void userControlCheckIn1_Load(object sender, EventArgs e)
        {

        }

        private void dgViewCoupleRoom_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        public void Condition()
        {
            try
            {
                for (int i = 0; i < dgViewCoupleRoom.Rows.Count; i++)
                {
                    int val = Convert.ToInt32(dgViewCoupleRoom.Rows[i].Cells[1].Value.ToString());
                    if (val == 1)
                    {
                        dgViewCoupleRoom.Rows[i].DefaultCellStyle.BackColor = Color.Green;

                    }
                    else if (val == 2)
                    {
                        dgViewCoupleRoom.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    else if (val == 3)
                    {
                        dgViewCoupleRoom.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }
            catch (Exception)
            {

            }
            dgViewCoupleRoom.CurrentRow.Selected = false;
            dgViewCoupleRoom.Enabled = false;
        }

        public void DeluxInfo()
        {
            try
            {
                for (int i = 0; i < dgViewDeluxRoom.Rows.Count; i++)
                {
                    int val = Convert.ToInt32(dgViewDeluxRoom.Rows[i].Cells[1].Value.ToString());
                    if (val == 1)
                    {
                        dgViewDeluxRoom.Rows[i].DefaultCellStyle.BackColor = Color.Green;

                    }
                    else if (val == 2)
                    {
                        dgViewDeluxRoom.Rows[i].DefaultCellStyle.BackColor = Color.Yellow;
                    }
                    else if (val == 3)
                    {
                        dgViewDeluxRoom.Rows[i].DefaultCellStyle.BackColor = Color.Red;
                    }
                }
            }
            catch (Exception)
            {

            }

            dgViewDeluxRoom.CurrentRow.Selected = false;
            dgViewDeluxRoom.Enabled = false;
        }

        private void txtBoxOccupied_TextChanged(object sender, EventArgs e)
        {

        }

        // Count Room Available, Free, Occupied and Pending ===================================
        public void CountRoom()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();
                string showFree = "SELECT COUNT(S) FROM CoupleRoom WHERE S = 1";
                SqlCommand cmdFree = new SqlCommand(showFree, con);
                int CplFree = Convert.ToInt32(cmdFree.ExecuteScalar());

                string showFreeDlx = "SELECT COUNT(S) FROM DeluxRoom WHERE S = 1";
                SqlCommand cmdFreeDlx = new SqlCommand(showFreeDlx, con);
                int DlxFree = Convert.ToInt32(cmdFreeDlx.ExecuteScalar());

                string showPending = "SELECT COUNT(S) FROM CoupleRoom WHERE S = 2";
                SqlCommand cmdPending = new SqlCommand(showPending, con);
                int CplPending = Convert.ToInt32(cmdPending.ExecuteScalar());

                string showPendingDlx = "SELECT COUNT(S) FROM DeluxRoom WHERE S = 2";
                SqlCommand cmdPendingDlx = new SqlCommand(showPendingDlx, con);
                int DlxPending = Convert.ToInt32(cmdPendingDlx.ExecuteScalar());

                string showOccupied = "SELECT COUNT(S) FROM CoupleRoom WHERE S = 3";
                SqlCommand cmdOccupied = new SqlCommand(showOccupied, con);
                int CplOccupied = Convert.ToInt32(cmdOccupied.ExecuteScalar());

                string showOccupiedDlx = "SELECT COUNT(S) FROM DeluxRoom WHERE S = 3";
                SqlCommand cmdOccupiedDlx = new SqlCommand(showOccupiedDlx, con);
                int DlxOccupied = Convert.ToInt32(cmdOccupiedDlx.ExecuteScalar());

                int totalFree = CplFree + DlxFree;
                int totalPending = CplPending + DlxPending;
                int totalOccupied = CplOccupied + DlxOccupied;
                int totalRoom = totalFree + totalPending + totalOccupied;

                txtBoxFree.Text = totalFree.ToString();
                txtBoxPending.Text = totalPending.ToString();
                txtBoxOccupied.Text = totalOccupied.ToString();
                txtBoxTotalRoom.Text = totalRoom.ToString();

                txtBoxCoupleOccupied.Text = CplOccupied.ToString();
                txtBoxCouplePending.Text = CplPending.ToString();
                txtBoxCoupleFree.Text = CplFree.ToString();

                txtBoxOccupiedDlx.Text = DlxOccupied.ToString();
                txtBoxPendingDlx.Text = DlxPending.ToString();
                txtBoxFreeDlx.Text = DlxFree.ToString();
                con.Close();
            }
        }


        // For fill the comboBox Room Type================================================================
        void Fillcombo()
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlDataAdapter d = new SqlDataAdapter("SELECT RoomName FROM RoomType", con);
            DataTable dt = new DataTable();
            d.Fill(dt);
            cmbBoxRoomType.DataSource = dt;
            cmbBoxRoomType.DisplayMember = "RoomName";
            con.Close();
        }

        void FillcomboPending()
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlDataAdapter d = new SqlDataAdapter("SELECT RoomNo FROM Reservation Where IsActive = 1", con);
            DataTable dt = new DataTable();
            d.Fill(dt);
            DataSet ds = new DataSet();
            cmbPendingRoomNo.DataSource = dt;
            cmbPendingRoomNo.DisplayMember = "RoomNo";
            con.Close();
        }

        void FillcomboPendingDelux()
        {
            //SqlConnection con = new SqlConnection(cs);
            //con.Open();
            //SqlDataAdapter d = new SqlDataAdapter("SELECT RoomNo FROM DeluxRoom WHERE S = 2", con);
            //DataTable dt = new DataTable();
            //d.Fill(dt);
            //cmbPendingRoomNo.DataSource = dt;
            //cmbPendingRoomNo.DisplayMember = "RoomNo";
            //con.Close();            
        }

        void FillcomboReservationAvailable()
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlDataAdapter d = new SqlDataAdapter("SELECT RoomName FROM RoomType", con);
            DataTable dt = new DataTable();
            d.Fill(dt);
            cmbReservationRoomType.DataSource = dt;
            cmbReservationRoomType.DisplayMember = "RoomName";
            con.Close();
        }

        private void cmbBoxRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbBoxRoomType.Text == "Couple Room")
            {
                SqlConnection con = new SqlConnection(cs);
                con.Open();
                SqlDataAdapter daRoomNo = new SqlDataAdapter("SELECT RoomNo FROM CoupleRoom WHERE S = 1", con);
                DataTable dtRoomNo = new DataTable();
                daRoomNo.Fill(dtRoomNo);
                cmbBoxRoomNo.DataSource = dtRoomNo;
                cmbBoxRoomNo.DisplayMember = "RoomNo";
                con.Close();
            }
            else if (cmbBoxRoomType.Text == "Delux Suit")
            {
                SqlConnection con = new SqlConnection(cs);
                con.Open();
                SqlDataAdapter daRoomNo = new SqlDataAdapter("SELECT RoomNo FROM DeluxRoom WHERE S = 1", con);
                DataTable dtRoomNo = new DataTable();
                daRoomNo.Fill(dtRoomNo);
                cmbBoxRoomNo.DataSource = dtRoomNo;
                cmbBoxRoomNo.DisplayMember = "RoomNo";
                con.Close();
            }
            else
            {
                SqlConnection con = new SqlConnection(cs);
                con.Open();
                SqlDataAdapter daRoomNo = new SqlDataAdapter("SELECT RoomNo FROM NoRoom", con);
                DataTable dtRoomNo = new DataTable();
                daRoomNo.Fill(dtRoomNo);
                cmbReservationRoomNo.DataSource = dtRoomNo;
                cmbReservationRoomNo.DisplayMember = "RoomNo";
                con.Close();
            }
        }



        // This is confirm Button For Check-IN =====================================================================
        private void button2_Click(object sender, EventArgs e)
        {
            if (txtCheckInName.Text != "" && txtCheckInPhone.Text != "" && cmbBoxRoomNo.Text != "")
            {
                if(cmbBoxRoomType.Text == "Delux Suit")
                {

                    try
                    {
                        SqlConnection con = new SqlConnection(cs);
                        con.Open();
                        SqlCommand cmd = new SqlCommand("sp_InsertCheckIn3", con); // Couple Room
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@name", SqlDbType.VarChar).Value = txtCheckInName.Text.Trim();
                        cmd.Parameters.AddWithValue("@fathername", SqlDbType.VarChar).Value = txtCheckInFatherName.Text.Trim();
                        cmd.Parameters.AddWithValue("@mothername", SqlDbType.VarChar).Value = txtCheckInMotherName.Text.Trim();
                        cmd.Parameters.AddWithValue("@nid", SqlDbType.VarChar).Value = txtCheckInNid.Text.Trim();
                        cmd.Parameters.AddWithValue("@cellphone", SqlDbType.VarChar).Value = txtCheckInPhone.Text.Trim();
                        cmd.Parameters.AddWithValue("@roomtype", SqlDbType.VarChar).Value = cmbBoxRoomType.Text.Trim();
                        cmd.Parameters.AddWithValue("@roomno", SqlDbType.Int).Value = cmbBoxRoomNo.Text.Trim();
                        cmd.Parameters.AddWithValue("@address", SqlDbType.VarChar).Value = rtxtBoxAddress.Text.Trim();
                        cmd.Parameters.AddWithValue("@noofperson", SqlDbType.VarChar).Value = txtCheckNoOfPerson.Text.Trim();
                        cmd.Parameters.AddWithValue("@phone01", SqlDbType.VarChar).Value = txtCheckInPhone01.Text.Trim();
                        cmd.Parameters.AddWithValue("@phone02", SqlDbType.VarChar).Value = txtCheckInPhone01.Text.Trim();
                        cmd.Parameters.AddWithValue("@operator", SqlDbType.VarChar).Value = txtShowCurrentUser.Text.Trim();
                        cmd.ExecuteNonQuery();
                        con.Close();


                        txtCheckInName.Text = "";
                        txtCheckInFatherName.Text = "";
                        txtCheckInMotherName.Text = "";
                        txtCheckInNid.Text = "";
                        txtCheckInPhone.Text = "";
                        cmbBoxRoomType.Text = "";
                        cmbBoxRoomNo.Text = "";
                        rtxtBoxAddress.Text = "";
                        txtCheckNoOfPerson.Text = "";
                        txtCheckInPhone01.Text = "";
                        txtCheckInPhone02.Text = "";
                        MessageBox.Show("Successfully check-in !!!");
                    }
                    catch (Exception er)
                    {
                        MessageBox.Show("Error Occur" + er.Message);
                    }
                }
                else if(cmbBoxRoomType.Text == "Couple Room")
                {
                    try
                    {
                        SqlConnection con = new SqlConnection(cs);
                        con.Open();
                        SqlCommand cmd = new SqlCommand("sp_InsertCheckIn4", con); // Delux Room
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@name", SqlDbType.VarChar).Value = txtCheckInName.Text.Trim();
                        cmd.Parameters.AddWithValue("@fathername", SqlDbType.VarChar).Value = txtCheckInFatherName.Text.Trim();
                        cmd.Parameters.AddWithValue("@mothername", SqlDbType.VarChar).Value = txtCheckInMotherName.Text.Trim();
                        cmd.Parameters.AddWithValue("@nid", SqlDbType.VarChar).Value = txtCheckInNid.Text.Trim();
                        cmd.Parameters.AddWithValue("@cellphone", SqlDbType.VarChar).Value = txtCheckInPhone.Text.Trim();
                        cmd.Parameters.AddWithValue("@roomtype", SqlDbType.VarChar).Value = cmbBoxRoomType.Text.Trim();
                        cmd.Parameters.AddWithValue("@roomno", SqlDbType.Int).Value = cmbBoxRoomNo.Text.Trim();
                        cmd.Parameters.AddWithValue("@address", SqlDbType.VarChar).Value = rtxtBoxAddress.Text.Trim();
                        cmd.Parameters.AddWithValue("@noofperson", SqlDbType.VarChar).Value = txtCheckNoOfPerson.Text.Trim();
                        cmd.Parameters.AddWithValue("@phone01", SqlDbType.VarChar).Value = txtCheckInPhone01.Text.Trim();
                        cmd.Parameters.AddWithValue("@phone02", SqlDbType.VarChar).Value = txtCheckInPhone01.Text.Trim();
                        cmd.Parameters.AddWithValue("@operator", SqlDbType.VarChar).Value = txtShowCurrentUser.Text.Trim();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        txtCheckInName.Text = "";
                        txtCheckInFatherName.Text = "";
                        txtCheckInMotherName.Text = "";
                        txtCheckInNid.Text = "";
                        txtCheckInPhone.Text = "";
                        cmbBoxRoomType.Text = "";
                        cmbBoxRoomNo.Text = "";
                        rtxtBoxAddress.Text = "";
                        txtCheckNoOfPerson.Text = "";
                        txtCheckInPhone01.Text = "";
                        txtCheckInPhone02.Text = "";
                        MessageBox.Show("Successfully check-in !!!");

                    }
                    catch (Exception er)
                    {
                        MessageBox.Show("Error Occur" + er.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Sorry, Selected Room Type OR Room Number is not found !!!");
                }
            }
            else
            {
                MessageBox.Show("Please Fill Mandatory Fields: Name, Cell Phone and Room Number !!!");
            }

        }


        // This is reset button ====================================================================.
        private void button1_Click(object sender, EventArgs e)
        {
            txtCheckInName.Text = "";
            txtCheckInFatherName.Text = "";
            txtCheckInMotherName.Text = "";
            txtCheckInNid.Text = "";
            txtCheckInPhone.Text = "";
            cmbBoxRoomType.Text = "";
            cmbBoxRoomNo.Text = "";
            rtxtBoxAddress.Text = "";
            txtCheckNoOfPerson.Text = "";
            txtCheckInPhone01.Text = "";
            txtCheckInPhone02.Text = "";
        }


        // Check-Out Search By Name and Room No.======================================================
        private void btnCheckOutSearch_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlDataAdapter daRoomNo = new SqlDataAdapter("SELECT * FROM CheckIn WHERE Name = '"+txtAutoSearchCheckOut.Text+ "' And IsActive = 1", con);
            DataTable dt = new DataTable();
            daRoomNo.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                //Where ColumnName is the Field from the DB that you want to display
                lblCheckOutName.Text = dt.Rows[0]["Name"].ToString();
                lblCheckOutCellPhone.Text = dt.Rows[0]["CellPhone"].ToString();
                lblCheckOutRoomType.Text = dt.Rows[0]["RoomType"].ToString();
                lblCheckOutRoomNo.Text = dt.Rows[0]["RoomNo"].ToString();
                lblCheckInID2.Text = dt.Rows[0]["CheckInID"].ToString();

                SqlDataAdapter daTotalDay = new SqlDataAdapter("SELECT ToalDays FROM InOut WHERE CellPhone = '" + lblCheckOutCellPhone.Text + "'", con);
                DataTable dataTable = new DataTable();
                daTotalDay.Fill(dataTable);
                int totalDay = Convert.ToInt32(dataTable.Rows[0]["ToalDays"]);

                if (totalDay <= 0)
                {
                    lblCheckOutTotalDay.Text = "1";
                }
                else
                {
                    lblCheckOutTotalDay.Text = dataTable.Rows[0]["ToalDays"].ToString();
                }
            }
            con.Close();
        }

        private void btnCheckOutSearchRoom_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlDataAdapter daRoomNo = new SqlDataAdapter("SELECT * FROM CheckIn WHERE RoomNo = '" + txtAutoSearchCheckOutRoom.Text + "' And IsActive = 1", con);
            DataTable dt = new DataTable();
            daRoomNo.Fill(dt);
            if (dt.Rows.Count > 0)
            {
                //Where ColumnName is the Field from the DB that you want to display
                lblCheckOutName.Text = dt.Rows[0]["Name"].ToString();
                lblCheckOutCellPhone.Text = dt.Rows[0]["CellPhone"].ToString();
                lblCheckOutRoomType.Text = dt.Rows[0]["RoomType"].ToString();
                lblCheckOutRoomNo.Text = dt.Rows[0]["RoomNo"].ToString();
                lblCheckInID2.Text = dt.Rows[0]["CheckInID"].ToString();

                SqlDataAdapter daTotalDay = new SqlDataAdapter("SELECT ToalDays FROM InOut WHERE CellPhone = '" + lblCheckOutCellPhone.Text + "'", con);
                DataTable dataTable = new DataTable();
                daTotalDay.Fill(dataTable);
                int totalDay = Convert.ToInt32(dataTable.Rows[0]["ToalDays"]);

                if (totalDay <=0)
                {
                    lblCheckOutTotalDay.Text = "1";
                }
                else
                {
                    lblCheckOutTotalDay.Text = dataTable.Rows[0]["ToalDays"].ToString();
                }
            }
            con.Close();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            txtAutoSearchCheckOut.Text = "";
            txtAutoSearchCheckOutRoom.Text = "";
            lblCheckOutName.Text = "";
            lblCheckOutCellPhone.Text = "";
            lblCheckOutRoomType.Text = "";
            lblCheckOutRoomNo.Text = "";
        }


        // Confirm Button for Check-Out ==========================================================================
        private void btnPrint_Click(object sender, EventArgs e)
        {          

            if(lblCheckOutRoomNo.Text != "")
            {
                if (lblCheckOutName.Text != "" && lblCheckOutCellPhone.Text != "" && lblCheckOutRoomType.Text != "" && lblCheckOutRoomNo.Text != "" && lblCheckOutTotalDay.Text != "")
                {
                    SqlConnection con = new SqlConnection(cs);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_TotalAmountCouple", con); // Couple Room - send data to calculation table
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@name", SqlDbType.VarChar).Value = lblCheckOutName.Text.Trim();
                    cmd.Parameters.AddWithValue("@cellphone", SqlDbType.VarChar).Value = lblCheckOutCellPhone.Text.Trim();
                    cmd.Parameters.AddWithValue("@roomtype", SqlDbType.VarChar).Value = lblCheckOutRoomType.Text.Trim();
                    cmd.Parameters.AddWithValue("@roomno", SqlDbType.VarChar).Value = lblCheckOutRoomNo.Text.Trim();
                    cmd.Parameters.AddWithValue("@checkinid", SqlDbType.Int).Value = lblCheckInID2.Text.Trim();
                    cmd.Parameters.AddWithValue("@totalday", SqlDbType.Int).Value = lblCheckOutTotalDay.Text.Trim();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }


                if(lblCheckOutRoomType.Text == "Couple Room")
                {
                    SqlConnection con = new SqlConnection(cs);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_CheckOutCoupleRoom", con); // Couple Room
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@roomno", SqlDbType.Int).Value = lblCheckOutRoomNo.Text.Trim();
                    cmd.Parameters.AddWithValue("@cellphone", SqlDbType.Int).Value = lblCheckOutCellPhone.Text.Trim();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }
                else if(lblCheckOutRoomType.Text == "Delux Suit")
                {
                    SqlConnection con = new SqlConnection(cs);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_CheckOutDeluxSuit", con); // Delux Room
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@roomno", SqlDbType.Int).Value = lblCheckOutRoomNo.Text.Trim();
                    cmd.Parameters.AddWithValue("@cellphone", SqlDbType.Int).Value = lblCheckOutCellPhone.Text.Trim();
                    cmd.ExecuteNonQuery();
                    con.Close();
                }

            }
            else
            {
                MessageBox.Show("Please select a name or room number !!");
            }

            txtAutoSearchCheckOut.Text = "";
            txtAutoSearchCheckOutRoom.Text = "";
            lblCheckOutName.Text = "";
            lblCheckOutCellPhone.Text = "";
            lblCheckOutRoomType.Text = "";
            lblCheckOutRoomNo.Text = "";
            lblCheckOutTotalDay.Text = "";
            lblCheckInID2.Text = "";
        }


        // ================================================
        private void inHouseGuestToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panDetailCheckOutWithBill.Visible = false;
            dgViewCheckInOut.Visible = false;
            dgViewInHouseGuest.Visible = true;
            this.inHouseGuestToolStripMenuItem.ForeColor = Color.Black;
            this.inHouseGuestToolStripMenuItem.BackColor = Color.White;
            this.checkInAndOutToolStripMenuItem.ForeColor = Color.White;
            this.checkInAndOutToolStripMenuItem.BackColor = Color.Black;
            panBillingReport.Visible = false;


            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string showDelux = "SELECT * FROM CheckIn Where IsActive = 1";
            SqlCommand cmdDelux = new SqlCommand(showDelux, con);
            cmdDelux = new SqlCommand(showDelux, con);
            DataTable delux = new DataTable();
            SqlDataAdapter dlx = new SqlDataAdapter(cmdDelux);
            dlx.Fill(delux);
            dgViewInHouseGuest.DataSource = delux.DefaultView;
            con.Close();
        }

        private void checkInAndOutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            dgViewInHouseGuest.Visible = false;
            dgViewCheckInOut.Visible = true;
            this.checkInAndOutToolStripMenuItem.ForeColor = Color.Black;
            this.checkInAndOutToolStripMenuItem.BackColor = Color.White;
            this.inHouseGuestToolStripMenuItem.ForeColor = Color.White;
            this.inHouseGuestToolStripMenuItem.BackColor = Color.Black;
            panBillingReport.Visible = false;
            panDetailCheckOutWithBill.Visible = false;



            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string showDelux = "SELECT * FROM InOut";
            SqlCommand cmdDelux = new SqlCommand(showDelux, con);
            cmdDelux = new SqlCommand(showDelux, con);
            DataTable delux = new DataTable();
            SqlDataAdapter dlx = new SqlDataAdapter(cmdDelux);
            dlx.Fill(delux);
            dgViewCheckInOut.DataSource = delux.DefaultView;
            con.Close();
        }

        private void printDocument1_PrintPage(object sender, System.Drawing.Printing.PrintPageEventArgs e)
        {

        }

        private void rptViewerCurrentGuest_Load(object sender, EventArgs e)
        {

        }

        private void panArchive_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnArchive_Click(object sender, EventArgs e)
        {
            panSelect.Height = btnArchive.Height;
            panSelect.Top = btnArchive.Top;

            panHome.Visible = true;
            panRoom.Visible = true;
            panCheckIn.Visible = true;
            panCheckOut.Visible = true;
            panReservation.Visible = true;
            panReport.Visible = true;
            panArchive.Visible = true;
            panCatering.Visible = false;


            rptViewerGuestInfoArchive.RefreshReport();
            rptViewerCurrentGuest.RefreshReport();
            rptViewerReservation.RefreshReport();


            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string gdView = "Select * from Calculation";
            cmd = new SqlCommand(gdView, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView2.DataSource = dt.DefaultView;
            con.Close();

            // Showing Billing Info====================Cascade

            con.Open();
            string gdView3 = "Select * from Calculation";
            cmd = new SqlCommand(gdView3, con);
            DataTable dt3 = new DataTable();
            SqlDataAdapter da3 = new SqlDataAdapter(cmd);
            da3.Fill(dt3);
            dgViewCascadeDeleteInfo.DataSource = dt3.DefaultView;
            con.Close();

            // Showing Edited Info Bill ==========================cascade
            SqlConnection con2 = new SqlConnection(cs);
            con2.Open();
            string gdView2 = "Select * from BillEditRecord";
            SqlCommand cmd2 = new SqlCommand(gdView2, con2);
            DataTable dt2 = new DataTable();
            SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
            da2.Fill(dt2);
            dgViewBillEditingInfo.DataSource = dt2.DefaultView;
            con2.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'myTestDB1DataSet3.BillEditRecord' table. You can move, or remove it, as needed.
            this.billEditRecordTableAdapter.Fill(this.myTestDB1DataSet3.BillEditRecord);
            // TODO: This line of code loads data into the 'myTestDB1DataSet2.Calculation' table. You can move, or remove it, as needed.
            this.calculationTableAdapter2.Fill(this.myTestDB1DataSet2.Calculation);
            // TODO: This line of code loads data into the 'myTestDB1DataSet1.Calculation' table. You can move, or remove it, as needed.
            this.calculationTableAdapter1.Fill(this.myTestDB1DataSet1.Calculation);
            // TODO: This line of code loads data into the 'myTestDB1DataSet.Calculation' table. You can move, or remove it, as needed.
            this.calculationTableAdapter.Fill(this.myTestDB1DataSet.Calculation);
            rptCurrentGuest viewer1 = new rptCurrentGuest();
            rptViewerCurrentGuest.ReportSource = viewer1;
            rptViewerCurrentGuest.RefreshReport();

            rptArchive viewer2 = new rptArchive();
            rptViewerGuestInfoArchive.ReportSource = viewer2;
            rptViewerGuestInfoArchive.RefreshReport();

            ReservationRecords viewer3 = new ReservationRecords();
            rptViewerReservation.ReportSource = viewer3;
            rptViewerReservation.RefreshReport();
        }

        private void label57_Click(object sender, EventArgs e)
        {

        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {

        }


        // ====================== For Reservation =========================
        private void btnReservationConfirm_Click(object sender, EventArgs e)
        {

            if (txtReservationName.Text != "" && txtReservationCellPhone.Text != "" && cmbReservationRoomNo.Text != "")
            {
                if (cmbReservationRoomType.Text == "Couple Room")
                {
                    try
                    {
                        SqlConnection con = new SqlConnection(cs);
                        con.Open();
                        SqlCommand cmd = new SqlCommand("sp_ReservationCouple", con); // Couple Room
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@name", SqlDbType.VarChar).Value = txtReservationName.Text.Trim();
                        cmd.Parameters.AddWithValue("@fathername", SqlDbType.VarChar).Value = txtReservationFatherName.Text.Trim();
                        cmd.Parameters.AddWithValue("@mothername", SqlDbType.VarChar).Value = txtReservationMotherName.Text.Trim();
                        cmd.Parameters.AddWithValue("@nid", SqlDbType.VarChar).Value = txtReservationNID.Text.Trim();
                        cmd.Parameters.AddWithValue("@cellphone", SqlDbType.VarChar).Value = txtReservationCellPhone.Text.Trim();
                        cmd.Parameters.AddWithValue("@roomtype", SqlDbType.VarChar).Value = cmbReservationRoomType.Text.Trim();
                        cmd.Parameters.AddWithValue("@roomno", SqlDbType.Int).Value = cmbReservationRoomNo.Text.Trim();
                        cmd.Parameters.AddWithValue("@address", SqlDbType.VarChar).Value = txtReservationAddress.Text.Trim();
                        cmd.Parameters.AddWithValue("@noofperson", SqlDbType.VarChar).Value = txtReservationNoOfPerson.Text.Trim();
                        cmd.Parameters.AddWithValue("@checkindate", SqlDbType.VarChar).Value = dtPickerReservationCheckIn.Text.Trim();
                        cmd.Parameters.AddWithValue("@checkoutdate", SqlDbType.VarChar).Value = dtPickerReservationCheckOut.Text.Trim();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        txtReservationName.Text = "";
                        txtReservationFatherName.Text = "";
                        txtReservationMotherName.Text = "";
                        txtReservationNID.Text = "";
                        txtReservationCellPhone.Text = "";
                        cmbReservationRoomType.Text = "";
                        cmbReservationRoomNo.Text = "";
                        txtReservationAddress.Text = "";
                        txtReservationNoOfPerson.Text = "";
                        dtPickerReservationCheckIn.Text = "";
                        dtPickerReservationCheckOut.Text = "";

                    }
                    catch (Exception er)
                    {
                        MessageBox.Show("Error Occur" + er.Message);
                    }
                }
                else if (cmbReservationRoomType.Text == "Delux Suit")
                {
                    try
                    {
                        SqlConnection con = new SqlConnection(cs);
                        con.Open();
                        SqlCommand cmd = new SqlCommand("sp_ReservationDelux", con); // Couple Room
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@name", SqlDbType.VarChar).Value = txtReservationName.Text.Trim();
                        cmd.Parameters.AddWithValue("@fathername", SqlDbType.VarChar).Value = txtReservationFatherName.Text.Trim();
                        cmd.Parameters.AddWithValue("@mothername", SqlDbType.VarChar).Value = txtReservationMotherName.Text.Trim();
                        cmd.Parameters.AddWithValue("@nid", SqlDbType.VarChar).Value = txtReservationNID.Text.Trim();
                        cmd.Parameters.AddWithValue("@cellphone", SqlDbType.VarChar).Value = txtReservationCellPhone.Text.Trim();
                        cmd.Parameters.AddWithValue("@roomtype", SqlDbType.VarChar).Value = cmbReservationRoomType.Text.Trim();
                        cmd.Parameters.AddWithValue("@roomno", SqlDbType.Int).Value = cmbReservationRoomNo.Text.Trim();
                        cmd.Parameters.AddWithValue("@address", SqlDbType.VarChar).Value = txtReservationAddress.Text.Trim();
                        cmd.Parameters.AddWithValue("@noofperson", SqlDbType.VarChar).Value = txtReservationNoOfPerson.Text.Trim();
                        cmd.Parameters.AddWithValue("@checkindate", SqlDbType.VarChar).Value = dtPickerReservationCheckIn.Text.Trim();
                        cmd.Parameters.AddWithValue("@checkoutdate", SqlDbType.VarChar).Value = dtPickerReservationCheckOut.Text.Trim();
                        cmd.ExecuteNonQuery();
                        con.Close();

                        txtReservationName.Text = "";
                        txtReservationFatherName.Text = "";
                        txtReservationMotherName.Text = "";
                        txtReservationNID.Text = "";
                        txtReservationCellPhone.Text = "";
                        cmbReservationRoomType.Text = "";
                        cmbReservationRoomNo.Text = "";
                        txtReservationAddress.Text = "";
                        txtReservationNoOfPerson.Text = "";
                        dtPickerReservationCheckIn.Text = "";
                        dtPickerReservationCheckOut.Text = "";

                    }
                    catch (Exception er)
                    {
                        MessageBox.Show("Error Occur" + er.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Sorry, Selected Room Type or Room No is not found !!!");
                }
            }
            else
            {
                MessageBox.Show("Please Fill Mandatory Fields : Name, CellPhone, RoomNo !!");
            }

            FillcomboPending();

        }

        //================= DropDown for Reservation Room Type And Room No
        private void cmbReservationRoomType_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cmbReservationRoomType.Text == "Couple Room")
            {
                SqlConnection con = new SqlConnection(cs);
                con.Open();
                SqlDataAdapter daRoomNo = new SqlDataAdapter("SELECT RoomNo FROM CoupleRoom WHERE S = 1", con);
                DataTable dtRoomNo = new DataTable();
                daRoomNo.Fill(dtRoomNo);
                cmbReservationRoomNo.DataSource = dtRoomNo;
                cmbReservationRoomNo.DisplayMember = "RoomNo";
                con.Close();
            }
            else if (cmbReservationRoomType.Text == "Delux Suit")
            {
                SqlConnection con = new SqlConnection(cs);
                con.Open();
                SqlDataAdapter daRoomNo = new SqlDataAdapter("SELECT RoomNo FROM DeluxRoom WHERE S = 1", con);
                DataTable dtRoomNo = new DataTable();
                daRoomNo.Fill(dtRoomNo);
                cmbReservationRoomNo.DataSource = dtRoomNo;
                cmbReservationRoomNo.DisplayMember = "RoomNo";
                con.Close();
            }
        }


        // =============== Pending Reservation Action ==================
        private void btnPendingActionSubmit_Click(object sender, EventArgs e)
        {
            if (cmbPendingRoomNo.Text != "" && cmbReservationAction.Text == "Confirm" && cmbPendingRoomNo.Text.StartsWith("10"))
            {
                try
                {
                    SqlConnection con = new SqlConnection(cs);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_PendingActionConfirm", con); // Couple Room
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@roomno", SqlDbType.Int).Value = cmbPendingRoomNo.Text.Trim();
                    cmd.Parameters.AddWithValue("@operator", SqlDbType.VarChar).Value = txtShowCurrentUser.Text.Trim();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    cmbPendingRoomNo.Text = "";
                    cmbReservationAction.Text = "";
                }
                catch (Exception)
                {
                    MessageBox.Show("Missmatch Transaction, Please try later !!");
                }
            }
            else if(cmbPendingRoomNo.Text != "" && cmbReservationAction.Text == "Confirm" && cmbPendingRoomNo.Text.StartsWith("20"))
            {
                try
                {
                    SqlConnection con = new SqlConnection(cs);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_PendingActionConfirmDeluxRoom", con); // Delux Room
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@roomno", SqlDbType.Int).Value = cmbPendingRoomNo.Text.Trim();
                    cmd.Parameters.AddWithValue("@operator", SqlDbType.VarChar).Value = txtShowCurrentUser.Text.Trim();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    cmbPendingRoomNo.Text = "";
                    cmbReservationAction.Text = "";
                }
                catch (Exception)
                {
                    MessageBox.Show("Missmatch Transaction, Please try later !!");
                }
            }
            else if (cmbPendingRoomNo.Text != "" && cmbReservationAction.Text == "Cancel" && cmbPendingRoomNo.Text.StartsWith("10"))
            {
                try
                {
                    SqlConnection con = new SqlConnection(cs);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_ReservationActionCancel", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@roomno", SqlDbType.Int).Value = cmbPendingRoomNo.Text.Trim();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    cmbPendingRoomNo.Text = "";
                    cmbReservationAction.Text = "";
                }
                catch (Exception)
                {
                    MessageBox.Show("Missmatch Transaction, Please try later !!");
                }
            }
            else if (cmbPendingRoomNo.Text != "" && cmbReservationAction.Text == "Cancel" && cmbPendingRoomNo.Text.StartsWith("20"))
            {
                try
                {
                    SqlConnection con = new SqlConnection(cs);
                    con.Open();
                    SqlCommand cmd = new SqlCommand("sp_ReservationActionCancelDelux", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@roomno", SqlDbType.Int).Value = cmbPendingRoomNo.Text.Trim();
                    cmd.ExecuteNonQuery();
                    con.Close();
                    cmbPendingRoomNo.Text = "";
                    cmbReservationAction.Text = "";
                }
                catch (Exception)
                {
                    MessageBox.Show("Missmatch Transaction, Please try later !!");
                }
            }
            else
            {
                MessageBox.Show("Please select room no and action type !!");
            }

            FillcomboPending();
        }


        // Log In
        private void btnLonin_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                if(txtUsername.Text !="" && txtPassword.Text != "")
                {
                    SqlDataAdapter cmd = new SqlDataAdapter("SELECT * FROM Users WHERE Username = '" + txtUsername.Text + "' AND Password = '" + txtPassword.Text + "';", con); // match with sql server user information.
                    DataTable dt = new DataTable();
                    cmd.Fill(dt);

                    if (dt.Rows.Count == 1)
                    {
                        panLogIn.Visible = false;
                        panHome.Visible = true;
                        panRoom.Visible = false;
                        panCheckIn.Visible = false;
                        panCheckOut.Visible = false;
                        panReservation.Visible = false;
                        panReport.Visible = false;
                        panArchive.Visible = false;





                        btnHome.Visible = true;
                        btnRoom.Visible = true;
                        btnCheckIn.Visible = true;
                        btnCheckOut.Visible = true;
                        btnReservation.Visible = true;
                        btnReport.Visible = true;
                        btnArchive.Visible = true;
                        btnCatering.Visible = false;

                        lblUser.Visible = true;
                        txtShowCurrentUser.Visible = true;
                        btnLogOff.Visible = true;
                        string user = dt.Rows[0]["Username"].ToString();
                        txtShowCurrentUser.Text = user;
                        MessageBox.Show("Welcome " + user + " !!!");
                    }
                    else
                    {
                        lblErrorMsg.Visible = true;
                        lblErrorMsg.Text = "Incorrect Username or Password !!!";
                    }
                }
                else
                {
                    lblErrorMsg.Visible = true;
                    lblErrorMsg.Text = "Please Enter Username and Password !!!";
                }
            }
        }

        private void btnLogOff_Click(object sender, EventArgs e)
        {
            panHome.Visible = true;
            panRoom.Visible = true;
            panCheckIn.Visible = true;
            panCheckOut.Visible = true;
            panReservation.Visible = true;
            panReport.Visible = true;
            panArchive.Visible = true;
            panCatering.Visible = true;
            panLogIn.Visible = true;


            btnHome.Visible = false;
            btnRoom.Visible = false;
            btnCheckIn.Visible = false;
            btnCheckOut.Visible = false;
            btnReservation.Visible = false;
            btnReport.Visible = false;
            btnArchive.Visible = false;
            btnCatering.Visible = false;
            lblUser.Visible = false;
            txtShowCurrentUser.Visible = false;
            btnLogOff.Visible = false;

            txtUsername.Text = "";
            txtPassword.Text = "";
            lblErrorMsg.Visible = false;
        }

        private void btnLoginReset_Click(object sender, EventArgs e)
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
            lblErrorMsg.Visible = false;
        }

        private void printBillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panDetailCheckOutWithBill.Visible = false;
            panBillingReport.Visible = true;
            panEditBillAndPrice.Visible = false;
            dgViewCheckInOut.Visible = false;
            dgViewInHouseGuest.Visible = false;


            try
            {
                SqlConnection con = new SqlConnection(cs);
                SqlCommand cmd = new SqlCommand("SELECT CheckInID FROM Calculation", con);
                con.Open();
                SqlDataAdapter dr = new SqlDataAdapter(cmd);
                AutoCompleteStringCollection myCollection = new AutoCompleteStringCollection();
                DataTable dt = new DataTable();
                dr.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        myCollection.Add(dt.Rows[i]["CheckInID"].ToString());
                    }
                }
                txtBillPrint.AutoCompleteCustomSource = myCollection;
            }
            catch
            {
                MessageBox.Show("Wrong!!");
            }
            // For Room
            

            rptBillPrint viewerBillPrint = new rptBillPrint();
            crystalReportViewerBillPrint.ReportSource = viewerBillPrint;
            crystalReportViewerBillPrint.RefreshReport();
        }

        private void billingReportToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void btnBillPrintSearch_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_BillPrintCouple", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@checkinid", SqlDbType.Int).Value = txtBillPrint.Text.Trim();
            cmd.ExecuteNonQuery();
            con.Close();
            crystalReportViewerBillPrint.RefreshReport();
            txtBillPrint.Text = "";
        }

        private void txtAutoSearchCheckOutRoom_TextChanged(object sender, EventArgs e)
        {

        }

        private void editBillToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panEditBillAndPrice.Visible = true;
            panDetailCheckOutWithBill.Visible = false;

        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_PriceUpdate", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@roomname", SqlDbType.VarChar).Value = cmbRoomTypeUpdatePrice.Text.Trim();
            cmd.Parameters.AddWithValue("@newprice", SqlDbType.Money).Value = txtNewPriceUpdate.Text.Trim();
            cmd.ExecuteNonQuery();
            con.Close();

            cmbRoomTypeUpdatePrice.Text = "";
            txtNewPriceUpdate.Text = "";
        }


        // ====== Edit Bill ========================================
        private void button5_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            SqlCommand cmd = new SqlCommand("sp_UpdateGuestBill", con);
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.AddWithValue("@checkinid", SqlDbType.Int).Value = txtGuestIDUpdate.Text.Trim();
            cmd.Parameters.AddWithValue("@discount", SqlDbType.Money).Value = txtAddDiscount.Text.Trim();
            cmd.Parameters.AddWithValue("@operator", SqlDbType.VarChar).Value = txtShowCurrentUser.Text.Trim();
            cmd.ExecuteNonQuery();
            con.Close();

            txtGuestIDUpdate.Text = "";
            txtAddDiscount.Text = "";
        }

        private void checkOutDetailsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            panDetailCheckOutWithBill.Visible = true;

            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string gdView = "Select * from Calculation";
            cmd = new SqlCommand(gdView, con);
            DataTable dt = new DataTable();
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            dataGridView1.DataSource = dt.DefaultView;
            con.Close();
        }

        private void btnCatering_Click(object sender, EventArgs e)
        {
            panHome.Visible = true;
            panRoom.Visible = true;
            panCheckIn.Visible = true;
            panCheckOut.Visible = true;
            panReservation.Visible = true;
            panReport.Visible = true;
            panArchive.Visible = true;
            panCatering.Visible = true;


        }


        // Cascading Delete Guest Bill ================================================
        private void btnCascadeDelete_Click(object sender, EventArgs e)
        {
            var confirmResult = MessageBox.Show("Are you sure to delete this item?\nAll The Child items related to this will Delete Permanently.",
                                     "Confirm Delete!!",
                                     MessageBoxButtons.YesNo);
            if (confirmResult == DialogResult.Yes)
            {
                try
                {

                    SqlConnection conn= new SqlConnection(cs);
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("DELETE FROM Calculation where ID = @id", conn);
                    cmd.Parameters.AddWithValue("@id", SqlDbType.Int).Value = Convert.ToInt32(txtCascadeDeleteIdBox.Text.Trim());
                    cmd.ExecuteNonQuery();
                    conn.Close();
                    MessageBox.Show("Data Successfully Deleted !!!");
                    txtCascadeDeleteIdBox.Text = "";
                }
                catch
                {
                    MessageBox.Show("Data not found !!");
                }

                // Showing Billing Info====================Cascade
                SqlConnection con = new SqlConnection(cs);
                con.Open();
                string gdView = "Select * from Calculation";
                cmd = new SqlCommand(gdView, con);
                DataTable dt = new DataTable();
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                dgViewCascadeDeleteInfo.DataSource = dt.DefaultView;
                con.Close();

                // Showing Edited Info Bill ==========================cascade
                SqlConnection con2 = new SqlConnection(cs);
                con2.Open();
                string gdView2 = "Select * from BillEditRecord";
                SqlCommand cmd2 = new SqlCommand(gdView2, con2);
                DataTable dt2 = new DataTable();
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                da2.Fill(dt2);
                dgViewBillEditingInfo.DataSource = dt2.DefaultView;
                con2.Close();
            }
            else
            {
                
            }
        }
    }
}


