using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace LINQ
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnProductName_Click(object sender, EventArgs e)
        {
            Product product = Linq2Objects.GetProduct(txtProductId.Text);
            //Need to bind to grid
            //Hence creating a temporary list for binding purposes
            List<Product> pList = new List<Product>();
            pList.Add(product);
            dataGridView1.DataSource = pList;
            
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //dataGridView1.DataSource = Linq2Objects.GroupProductsByCategory();
            dataGridView1.DataSource = Linq2Objects.Joins().ToList();
        }

        private void btnOrderBy_Click(object sender, EventArgs e)
        {
            dataGridView1.DataSource=Linq2Objects.OrderByProducts();
        }

        private void btnGroupBy_Click(object sender, EventArgs e)
        {
            foreach (var group in Linq2Objects.GroupBy())
            {
                //Console.WriteLine(group.Key == 0 ? "\nEven numbers:" : "\nOdd numbers:");
                foreach (int i in group)
                    lstGroupBy.Items.Add(i);
            }
        }
    }
}
