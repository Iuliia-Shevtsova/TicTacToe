using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp3
{
    public partial class TicTacToe : Form
    {
        Button[,] field = new Button[10, 10];
        int i;
        int j;
        int size;
        int w;
        int h;
        int x;
        int y;

        public TicTacToe()
        {
            InitializeComponent();
            w = 300;
            h = 200;
        }
        void field_Click(object sender, EventArgs e)
        {
            int index = (int)(sender as Button).Tag;
            int i = index % 10;
            int j = index / 10;

            MoveOfGamerMan(i, j);
            if (Winner("X"))
            {
                MessageBox.Show("You win");
                for (x = 0; x < size; x++)
                {
                    for (y = 0; y < size; y++)
                    { field[x, y].Text = ""; }
                }
            }
            else if ((FieldIsFull(i, j)) && (!Winner("X")) && (!Winner("O")))
            {
                MessageBox.Show("Draw"); for (x = 0; x < size; x++)
                {
                    for (y = 0; y < size; y++)
                    { field[x, y].Text = ""; }
                }
            }

            else MoveOfGamerComp();
            if ((Winner("O")))
            {
                MessageBox.Show("You lose"); for (x = 0; x < size; x++)
                {
                    for (y = 0; y < size; y++)
                    { field[x, y].Text = ""; }
                }
            }
            else if ((FieldIsFull(i, j)) && (!Winner("X")) && (!Winner("O")))
            {
                MessageBox.Show("Draw"); for (x = 0; x < size; x++)
                {
                    for (y = 0; y < size; y++)
                    { field[x, y].Text = ""; }
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            bool tryAgain = true;  // initial parameter for the loop
            var button1 = (Button)sender;
            if (button1 != null)
            {
                while (tryAgain)  // loop for finding of exceptions
                {
                    try
                    {
                        size = int.Parse(textBox1.Text); // enter field size by user
                        tryAgain = false; // if entrance value is integer the loop is end
                    }
                    catch (FormatException ex) // finding of exceptions
                    {//message about type of exception 

                        DialogResult result =
                            MessageBox.Show("Please, enter the whole number", "Mistake",
                            MessageBoxButtons.OK, MessageBoxIcon.Error);
                        if (result == DialogResult.OK)
                            Close();
                        //   MessageBox.Show("Mistake");
                        tryAgain = true; // the loop for searching of exceptions repeats again  
                    }
                }


                textBox1.Dispose();
                label1.Dispose();
                button1.Dispose();
                x = size * 45 + 10 - 300;
                y = size * 45 + 10 - 200;
            }
            this.ClientSize = new System.Drawing.Size(w + x, h + y);
            PrintField();
        }

        private void PrintField()
        {
            for (i = 0; i < size; i++)
            {
                for (j = 0; j < size; j++)
                {
                    field[i, j] = new Button();
                    field[i, j].Text = "";
                    field[i, j].Top = j * 45 + 10;
                    field[i, j].Left = i * 45 + 10;
                    field[i, j].BackColor = Color.White;
                    field[i, j].Size = new Size(40, 40);
                    field[i, j].Click += new EventHandler(field_Click);
                    field[i, j].Tag = j * 10 + i;
                    this.Controls.Add(field[i, j]);
                }
            }
        }

        private void MoveOfGamerMan(int x, int y)
        {
            if (field[x, y].Text == "")
                field[x, y].Text = "X";

        }

        private void MoveOfGamerComp()
        {
            bool condition = true;
            while (condition)
            {
                if (WinAndBlock()) condition = false;
                else if (AddOtoLine()) condition = false;
                else if (RandomO()) condition = false;
            }
        }

        private Boolean AddOtoLine()
        {
            int[] sumRow = new int[size];
            int[] sumColumn = new int[size];
            int[] sumMainDiag = new int[size];
            int[] sumSecDiag = new int[size];
            int yRow = 0;
            int xColumn = 0;
            int xyMainDiag = 0;
            int xSecDiag = 0;
            int ySecDiag = 0;
            int xRowFree = 0;
            int yRowFree = 0;
            int xColumnFree = 0;
            int yColumnFree = 0;
            int xyMainDiagFree = 0;
            int xSecDiagFree = 0;
            int ySecDiagFree = 0;
            int countXrow = 0;
            int countXcolumn = 0;
            int countOrow = 0;
            int countOcolumn = 0;
            int countXmainDiag = 0;
            int countOmainDiag = 0;
            int countXsecondDiag = 0;
            int countOsecondDiag = 0;
            int maxRow = 0;
            int maxColumn = 0;
            int maxMainDiag = 0;
            int maxSecDiag = 0;

            for (x = 0; x < size; x++)
            {
                for (y = 0; y < size; y++)
                {
                    if (field[x, y].Text == "X") countXrow++; //quantity "X" in a raw
                    if (field[x, y].Text == "O") countOrow++;//quantity "O" in a column
                    if (field[y, x].Text == "X") countXcolumn++;  //quantity "X" in a raw                  
                    if (field[y, x].Text == "O") countOcolumn++;//quantity "O" in a column
                    if (field[y, y].Text == "X") countXmainDiag++;  //quantity "X" in a main diagonal  
                    if (field[y, y].Text == "O") countOmainDiag++;  //quantity "O" in a main diagonal 
                    if (field[y, size - y - 1].Text == "X") countXsecondDiag++;//quantity "X" in a second diagonal
                    if (field[y, size - y - 1].Text == "O") countOsecondDiag++;//quantity "O" in a second diagonal
                    if (field[x, y].Text == "") yRow = y;
                    if (field[y, x].Text == "") xColumn = y;
                    if (field[y, y].Text == "") xyMainDiag = y;
                    if (field[y, size - y - 1].Text == "") xSecDiag = y; ySecDiag = size - y - 1;
                }
                //searcing max cell in the row or in the column
                if (countXrow == 0) // if there are no "X"
                { // searching max row in a rows and free cell
                    sumRow[x] = countOrow; //put cells "O" of a row in the array
                    if (sumRow[x] > maxRow)
                    {
                        maxRow = sumRow[x];
                        xRowFree = x; yRowFree = yRow; //remember tne coordinates of free cell in such row
                    }
                }
                if (countXcolumn == 0) // if there are no "X"
                { // searching max column in a columns and free cell
                    sumColumn[x] = countOcolumn; //put cells "O" of a column in the array
                    if (sumColumn[x] > maxColumn)
                    {
                        maxColumn = sumColumn[x];
                        xColumnFree = xColumn; yColumnFree = x;// remember tne coordinates of free cell in such column
                    }
                }
                if (countXmainDiag == 0) // if there are no "X"
                { // searching max column in a columns and free cell
                    sumMainDiag[x] = countOmainDiag; //put cells "O" of a column in the array
                    if (sumMainDiag[x] > maxMainDiag)
                    {
                        maxMainDiag = sumMainDiag[x];
                        xyMainDiagFree = xyMainDiag;// remember tne coordinates of free cell in such column
                    }
                }
                if (countXsecondDiag == 0) // if there are no "X"
                { // searching max column in a columns and free cell
                    sumSecDiag[x] = countOmainDiag; //put cells "O" of a column in the array
                    if (sumSecDiag[x] > maxSecDiag)
                    {
                        maxSecDiag = sumSecDiag[x];
                        xSecDiagFree = xSecDiag; ySecDiagFree = ySecDiag;// remember tne coordinates of free cell in such column
                    }
                }
            }
            if (field[xRowFree, yRowFree].Text == "") { field[xRowFree, yRowFree].Text = "O"; return true; }
            else if (field[xColumnFree, yColumnFree].Text == "") { field[xColumnFree, yColumnFree].Text = "O"; return true; }
            else if (field[xyMainDiagFree, xyMainDiagFree].Text == "") { field[xyMainDiagFree, xyMainDiagFree].Text = "O"; return true; }
            else if (field[xSecDiagFree, ySecDiagFree].Text == "") { field[xSecDiagFree, ySecDiagFree].Text = "O"; return true; }
            else return false;
        }


        private Boolean WinAndBlock()
        {
            // Strategy Win: If the player "O" has 2 in a row, or 2 in a column, or 2 in a diagonal, 
            //               he can place "O" in the therd in line to get 3 in a row, or 3 in a column, or 3 in a diagonal. 
            // Strategy Block: If the player "X" has 2 in a row, or 2 in a column, or 2 in a diagonal, 
            //                 player "O" have to place "O" in the therd in a corresponding line to block win of player "X". 
            int xRow = 0;
            int yRow = 0;
            int xColumn = 0;
            int yColumn = 0;
            int xMainDiagonal = 0;
            int yMainDiagonal = 0;
            int xSecDiagonal = 0;
            int ySecDiagonal = 0;

            int countRowO;
            int countColumnO;
            int countMainDiagonalO = 0;
            int countSecDiagonalO = 0;
            int countRowX;
            int countColumnX;
            int countMainDiagonalX = 0;
            int countSecDiagonalX = 0;
            int countFreeRow;
            int countFreeColumn;
            int countFreeMainDiagonal = 0;
            int countFreeSecDiagonal = 0;

            for (x = 0; x < size; x++)
            {
                countRowO = 0;
                countColumnO = 0;
                countRowX = 0;
                countColumnX = 0;
                countFreeRow = 0;
                countFreeColumn = 0;
                for (y = 0; y < size; y++)
                {  // checking raws
                    if (field[x, y].Text == "O") countRowO++;
                    if (field[x, y].Text == "X") countRowX++;
                    if (field[x, y].Text == "") { countFreeRow++; xRow = x; yRow = y; }
                    // checking Columns
                    if (field[y, x].Text == "O") countColumnO++;
                    if (field[y, x].Text == "X") countColumnX++;
                    if (field[y, x].Text == "") { countFreeColumn++; xColumn = y; yColumn = x; }
                    // checking Main Diagonal
                    if (field[y, y].Text == "O") countMainDiagonalO++;
                    if (field[y, y].Text == "X") countMainDiagonalX++;
                    if (field[y, y].Text == "") { countFreeMainDiagonal++; xMainDiagonal = y; yMainDiagonal = y; }
                    // checking Second Diagonal
                    if (field[y, size - y - 1].Text == "O") countSecDiagonalO++;
                    if (field[y, size - y - 1].Text == "X") countSecDiagonalX++;
                    if (field[y, size - y - 1].Text == "")
                    { countFreeSecDiagonal++; xSecDiagonal = y; ySecDiagonal = size - y - 1; }
                }

                if ((countRowO == (size - 1)) & (countFreeRow == 1)) // If the Gamer "O" has two in a row
                { field[xRow, yRow].Text = "O"; return true; }
                else if ((countColumnO == (size - 1)) & (countFreeColumn == 1)) // If the Gamer "O" has two in a Column
                { field[xColumn, yColumn].Text = "O"; return true; }
                else if ((countMainDiagonalO == (size - 1)) & (countFreeMainDiagonal == 1)) // If the Gamer "X" has two in the Main Diagonal
                { field[xMainDiagonal, yMainDiagonal].Text = "O"; return true; }
                else if ((countSecDiagonalO == (size - 1)) & (countFreeSecDiagonal == 1)) // If the Gamer "X" has two in the Second Diagonal
                { field[xSecDiagonal, ySecDiagonal].Text = "O"; return true; }

                else if ((countRowX == (size - 1)) & (countFreeRow == 1)) // If the Gamer "X" has two in a row
                { field[xRow, yRow].Text = "O"; return true; }
                else if ((countColumnX == (size - 1)) & (countFreeColumn == 1)) // If the Gamer "X" has two in a Column
                { field[xColumn, yColumn].Text = "O"; return true; }
                else if ((countMainDiagonalX == (size - 1)) & (countFreeMainDiagonal == 1)) // If the Gamer "X" has two in the Main Diagonal
                { field[xMainDiagonal, yMainDiagonal].Text = "O"; return true; }
                else if ((countSecDiagonalX == (size - 1)) & (countFreeSecDiagonal == 1)) // If the Gamer "X" has two in the Second Diagonal
                { field[xSecDiagonal, ySecDiagonal].Text = "O"; return true; }
            }
            return false;
        }

        private Boolean RandomO()
        {
            bool a;
            do
            {
                Random ran = new Random();
                x = ran.Next(size);
                y = ran.Next(size);
                if (field[x, y].Text == "")
                {
                    field[x, y].Text = "O";
                    return true;
                }
                else a = true;
            } while (a);
            return false;
        }

        private Boolean Winner(string z)
        {
            int countmain = 0;
            int countsecond = 0;
            int countrow;
            int countcolumn;

            // check whether cells in each row and in each column are filled in
            for (x = 0; x < size; x++)
            {
                countrow = 0;
                countcolumn = 0;
                for (y = 0; y < size; y++)
                {
                    if (field[x, y].Text == z) countrow++;
                    if (field[y, x].Text == z) countcolumn++;
                }
                if ((countrow == size) || (countcolumn == size)) return true;
            }

            // check whether main and secondary diagonal cells are filled in
            for (x = 0; x < size; x++)
            {
                if (field[x, x].Text == z) countmain++;
                if (field[x, size - x - 1].Text == z) countsecond++;
            }
            if ((countmain == size) || (countsecond == size)) return true;
            return false;

        }

        public Boolean FieldIsFull(int i, int j) // filling in the field by elements "-"
        {
            for (i = 0; i < size; i++)
            {
                for (j = 0; j < size; j++)
                {
                    if (field[i, j].Text == "") return false;
                }
            }
            return true;

        }


    }
}
