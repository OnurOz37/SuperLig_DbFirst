using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Linq; 

namespace SuperLigForm
{
    public partial class Form1 : Form
    {
        SuperLigEntities context = new SuperLigEntities(); 
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            dataGridViewTakim.DataSource = (from tak in context.Takim orderby tak.PuanDurumu descending 
                                            select new
                                            {
                                                tak.TakimID,
                                                tak.TakimAdi,
                                                tak.OynadigiMac,
                                                tak.PuanDurumu
                                            }).ToList(); 

        }

        private void dataGridViewTakim_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;

            DataGridViewRow row = dataGridViewTakim.Rows[rowIndex];

            label1.Text = row.Cells[0].Value.ToString();

            int takId = Convert.ToInt32(row.Cells[0].Value);
            textBox1.Text = row.Cells[1].Value.ToString();
            textBox2.Text = row.Cells[3].Value.ToString();

            var result = from f in context.Futbolcu 
                         join tak in context.Takim on f.TakimID equals tak.TakimID
                         where f.TakimID == takId
                         orderby f.Yas descending

                         select new
                         {
                             f.futbolcuID,
                             f.Adi,
                             f.Yas,
                             f.TakimID,
                             f.Takim,
                             f.Ulke

                         };
            dataGridViewFutbolcular.DataSource = result.ToList();

            var result2 = from a in context.Antrenor
                          join tak in context.Takim on a.TakimID equals tak.TakimID
                          where a.TakimID == takId
                          select new
                          {
                              a.antrenorID,
                              a.Adi,
                              a.Ulke,
                              a.CalistidigiTakimSayisi,
                              a.CalistiridigiTakim
                              
                          };
            dataGridViewAntrenor.DataSource = result2.ToList(); 
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dataGridViewFutbolcular_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;

            DataGridViewRow row = dataGridViewFutbolcular.Rows[rowIndex];

            label4.Text = row.Cells[0].Value.ToString();

            int futId = Convert.ToInt32(row.Cells[0].Value);

            textBox3.Text = row.Cells[1].Value.ToString();
            textBox4.Text = row.Cells[2].Value.ToString();
            textBox5.Text = row.Cells[4].Value.ToString();
            textBox9.Text = row.Cells[5].Value.ToString(); 

           
        }

        private void dataGridViewAntrenor_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            int rowIndex = e.RowIndex;

            DataGridViewRow row = dataGridViewAntrenor.Rows[rowIndex];

            textBox6.Text = row.Cells[1].Value.ToString();
            textBox8.Text = row.Cells[2].Value.ToString();
            textBox7.Text = row.Cells[4].Value.ToString();

        }
    }
}
