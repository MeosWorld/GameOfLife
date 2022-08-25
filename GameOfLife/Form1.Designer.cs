using System;
using System.Drawing;
using System.Windows.Forms;
using System.Windows.Forms.VisualStyles;

namespace GameOfLife
{
    partial class Form1
    {
        private static int scale = 25;
        private static int quantity = 7;

        private static int probability = 30;

        private int zaehlerSpalte = 1;
        private int zaehlerZeile = 1;


        private Button[,] buttons1 = new Button[quantity, quantity];
        private Button[,] buttons2 = new Button[quantity, quantity];

        /// <summary>
        /// Erforderliche Designervariable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Verwendete Ressourcen bereinigen.
        /// </summary>
        /// <param name="disposing">True, wenn verwaltete Ressourcen gelöscht werden sollen; andernfalls False.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Vom Windows Form-Designer generierter Code

        /// <summary>
        /// Erforderliche Methode für die Designerunterstützung.
        /// Der Inhalt der Methode darf nicht mit dem Code-Editor geändert werden.
        /// </summary>
        private void InitializeComponent()
        {            
            this.BtnStart = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // BtnStart
            // 
            this.BtnStart.Location = new System.Drawing.Point(275, 0);
            this.BtnStart.Name = "BtnStart";
            this.BtnStart.Size = new System.Drawing.Size(100, 25);
            this.BtnStart.TabIndex = 0;
            this.BtnStart.Text = "Start";
            this.BtnStart.UseVisualStyleBackColor = true;
            this.BtnStart.Click += new System.EventHandler(this.button2_Click);
            // 
            // button2
            // 
            this.button2.BackColor = System.Drawing.SystemColors.Control;
            this.button2.Location = new System.Drawing.Point(382, 0);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 1;
            this.button2.Text = "button2";
            this.button2.UseVisualStyleBackColor = false;
            this.button2.Click += new System.EventHandler(this.button2_Click_1);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.ClientSize = new System.Drawing.Size(669, 671);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.BtnStart);
            this.Name = "Form1";
            this.Text = "Form1";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);

        }

        private void AddButton1()
        {
            while (this.zaehlerZeile <= quantity)
            {
                this.button1 = new System.Windows.Forms.Button();
                this.SuspendLayout();

                this.Controls.Add(this.button1);
                // 
                // button1
                // 
                this.button1.BackColor = System.Drawing.SystemColors.WindowText;
                this.button1.Location = new System.Drawing.Point(scale * this.zaehlerSpalte, scale * this.zaehlerZeile);
                this.button1.Name = "button1";
                this.button1.Size = new System.Drawing.Size(25, 25);
                this.button1.TabIndex = 0;
                this.button1.UseVisualStyleBackColor = false;

                this.buttons1[this.zaehlerSpalte - 1, this.zaehlerZeile - 1] = this.button1;

                if (this.zaehlerSpalte == quantity)
                {
                    this.zaehlerZeile++;
                    this.zaehlerSpalte = 1;
                }
                else
                {
                    this.zaehlerSpalte++;
                }
            }

            this.zaehlerZeile = 1;
            this.zaehlerSpalte = 1;
            this.AddButton2();
        }

        private void AddButton2()
        {
            while (this.zaehlerZeile <= quantity)
            {
                this.button1 = new System.Windows.Forms.Button();
                this.SuspendLayout();

                this.Controls.Add(this.button1);
                // 
                // button1
                // 
                this.button1.BackColor = System.Drawing.SystemColors.Window;
                this.button1.Location = new System.Drawing.Point(scale * this.zaehlerSpalte, scale * this.zaehlerZeile);
                this.button1.Name = "button1";
                this.button1.Size = new System.Drawing.Size(scale, scale);
                this.button1.TabIndex = 0;
                this.button1.UseVisualStyleBackColor = false;

                this.buttons2[this.zaehlerSpalte - 1, this.zaehlerZeile - 1] = this.button1;

                if (this.zaehlerSpalte == quantity)
                {
                    this.zaehlerZeile++;
                    this.zaehlerSpalte = 1;
                }
                else
                {
                    this.zaehlerSpalte++;
                }
            }
        } 

        private void ChangeVisibleButtons(Button[,] but1, Button[,] but2)
        {
            bool visible = true;
            foreach(Button button in but1)
            {
                button.Visible = visible;
            }
            foreach(Button button in but2)
            {
                button.Visible = !visible;
            }
        }

        private void Next(bool next, bool newRandom)
        {
            if(next)
            {
                if(newRandom)
                {
                    NewRandom(this.buttons1);
                }
                ChangeStatus(this.buttons1, this.buttons2);
                ChangeVisibleButtons(this.buttons2,this.buttons1);
            }
            else
            {
                if (newRandom)
                {
                    NewRandom(this.buttons2);
                }
                ChangeStatus(this.buttons2, this.buttons1);
                ChangeVisibleButtons(this.buttons1, this.buttons2);
            }
        }
        private void ChangeStatus(Button[,] but1, Button[,] but2)
        {
            bool changer;
            for(int i = 0; i < quantity; i++)
            {
                for(int j = 0; j < quantity; j++)
                {
                    changer = NewStatus(CheckNeighbors(but1, i, j), but1[i, j].BackColor == System.Drawing.SystemColors.WindowText);

                    if(changer)
                    {
                        but2[i, j].BackColor = System.Drawing.SystemColors.WindowText;
                    }
                    else
                    {
                        but2[i, j].BackColor = System.Drawing.SystemColors.Window;
                    }
                }
            }
        }

        private int CheckNeighbors(Button[,] actBut, int spalte, int zeile)
        {
            int checkZeile, checkSpalte;
            int counter = 0;

            for(int i = -1; i < 2; i++)
            {
                for(int j = -1; j < 2; j++)
                {
                    if (!(i == 0 && j == 0))
                    {
                        #region CheckingEdges
                        if (spalte + i < 0)
                        {
                            checkSpalte = quantity - 1;
                        }
                        else if (spalte + i > quantity - 1)
                        {
                            checkSpalte = 0;
                        }
                        else
                        {
                            checkSpalte = spalte + i;
                        }
                        if (zeile + j < 0)
                        {
                            checkZeile = quantity - 1;
                        }
                        else if (zeile + j > quantity - 1)
                        {
                            checkZeile = 0;
                        }
                        else
                        {
                            checkZeile = zeile + j;
                        }
                        #endregion

                        if (actBut[checkSpalte, checkZeile].BackColor == System.Drawing.SystemColors.WindowText)
                        {
                            counter++;
                        }
                    }
                }
            }
            return counter;
        }

        private bool NewStatus(int counter, bool status)
        {
            if (status == true)
            {
                if(counter < 2 || counter > 3)
                {
                    status = false;
                }
                else
                {
                    status = true;
                }
            }
            else
            {
                if(counter == 3)
                {
                    status = true;
                }
                else
                {
                    status = false;
                }
            }
            return status;
        }

        private void NewRandom(Button[,] btn)
        {
            Random rnd = new Random();
            int pruefzahl;

            foreach(Button button in btn)
            {
                pruefzahl = rnd.Next(1,100);

                if(pruefzahl <= probability)
                {
                    button.BackColor = System.Drawing.SystemColors.WindowText;
                }
                else
                {
                    button.BackColor = System.Drawing.SystemColors.Window;
                }
            }

            //buttons1[1, 0].BackColor = System.Drawing.SystemColors.WindowText;
            //buttons1[2, 1].BackColor = System.Drawing.SystemColors.WindowText;
            //buttons1[0, 2].BackColor = System.Drawing.SystemColors.WindowText;
            //buttons1[1, 2].BackColor = System.Drawing.SystemColors.WindowText;
            //buttons1[2, 2].BackColor = System.Drawing.SystemColors.WindowText;
        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button BtnStart;
        private Button button2;
    }
}

