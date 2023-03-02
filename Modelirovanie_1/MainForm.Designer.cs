
namespace Modelirovanie_1
{
    partial class MainForm
    {
        /// <summary>
        /// Обязательная переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose (bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.but_input = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.radioButton1 = new System.Windows.Forms.RadioButton();
            this.radioButton2 = new System.Windows.Forms.RadioButton();
            this.label_input = new System.Windows.Forms.Label();
            this.label_postfix_symbol = new System.Windows.Forms.Label();
            this.buttonTact = new System.Windows.Forms.Button();
            this.button3 = new System.Windows.Forms.Button();
            this.label_stack = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label_input_change = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // but_input
            // 
            this.but_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.but_input.Location = new System.Drawing.Point(1077, 27);
            this.but_input.Name = "but_input";
            this.but_input.Size = new System.Drawing.Size(299, 86);
            this.but_input.TabIndex = 0;
            this.but_input.Text = "Задать";
            this.but_input.UseVisualStyleBackColor = true;
            this.but_input.Click += new System.EventHandler(this.button_to_input);
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(957, 125);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(419, 60);
            this.label1.TabIndex = 1;
            this.label1.Text = "Входная строка (в инфиксной форме)";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(50, 9);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(517, 45);
            this.label2.TabIndex = 2;
            this.label2.Text = "Выходная строка (в постфиксной форме)";
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(50, 165);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(168, 39);
            this.label3.TabIndex = 3;
            this.label3.Text = "Стек";
            this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label5
            // 
            this.label5.Enabled = false;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label5.Location = new System.Drawing.Point(50, 699);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(168, 23);
            this.label5.TabIndex = 5;
            this.label5.Text = "Режимы:";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // radioButton1
            // 
            this.radioButton1.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButton1.Location = new System.Drawing.Point(50, 726);
            this.radioButton1.Name = "radioButton1";
            this.radioButton1.Size = new System.Drawing.Size(260, 24);
            this.radioButton1.TabIndex = 6;
            this.radioButton1.TabStop = true;
            this.radioButton1.Text = "Пошаговый (управляемый пользоавателем)";
            this.radioButton1.UseVisualStyleBackColor = true;
            this.radioButton1.CheckedChanged += new System.EventHandler(this.radioButton_Step);
            // 
            // radioButton2
            // 
            this.radioButton2.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.radioButton2.Location = new System.Drawing.Point(50, 753);
            this.radioButton2.Name = "radioButton2";
            this.radioButton2.Size = new System.Drawing.Size(260, 24);
            this.radioButton2.TabIndex = 7;
            this.radioButton2.TabStop = true;
            this.radioButton2.Text = "Автоматический";
            this.radioButton2.UseVisualStyleBackColor = true;
            this.radioButton2.CheckedChanged += new System.EventHandler(this.radioButton_Auto);
            // 
            // label_input
            // 
            this.label_input.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label_input.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_input.Location = new System.Drawing.Point(957, 185);
            this.label_input.Name = "label_input";
            this.label_input.Size = new System.Drawing.Size(419, 57);
            this.label_input.TabIndex = 8;
            this.label_input.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_input.Click += new System.EventHandler(this.label_input_main);
            // 
            // label_postfix_symbol
            // 
            this.label_postfix_symbol.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label_postfix_symbol.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_postfix_symbol.Location = new System.Drawing.Point(50, 108);
            this.label_postfix_symbol.Name = "label_postfix_symbol";
            this.label_postfix_symbol.Size = new System.Drawing.Size(343, 45);
            this.label_postfix_symbol.TabIndex = 9;
            // 
            // buttonTact
            // 
            this.buttonTact.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.buttonTact.Location = new System.Drawing.Point(985, 699);
            this.buttonTact.Name = "buttonTact";
            this.buttonTact.Size = new System.Drawing.Size(150, 76);
            this.buttonTact.TabIndex = 10;
            this.buttonTact.Text = "Такт";
            this.buttonTact.UseVisualStyleBackColor = true;
            this.buttonTact.Click += new System.EventHandler(this.button_Tact);
            // 
            // button3
            // 
            this.button3.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.button3.Location = new System.Drawing.Point(1226, 700);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(150, 76);
            this.button3.TabIndex = 11;
            this.button3.Text = "Старт";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Click += new System.EventHandler(this.button_Start);
            // 
            // label_stack
            // 
            this.label_stack.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label_stack.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_stack.Location = new System.Drawing.Point(50, 204);
            this.label_stack.Name = "label_stack";
            this.label_stack.Size = new System.Drawing.Size(168, 477);
            this.label_stack.TabIndex = 13;
            this.label_stack.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label4.Location = new System.Drawing.Point(957, 261);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(419, 46);
            this.label4.TabIndex = 14;
            this.label4.Text = "Исходная строка (динамическая)";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label6.Location = new System.Drawing.Point(50, 67);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(343, 41);
            this.label6.TabIndex = 15;
            this.label6.Text = "С символами";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // label_input_change
            // 
            this.label_input_change.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.label_input_change.Font = new System.Drawing.Font("Microsoft Sans Serif", 15.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label_input_change.Location = new System.Drawing.Point(957, 307);
            this.label_input_change.Name = "label_input_change";
            this.label_input_change.Size = new System.Drawing.Size(419, 65);
            this.label_input_change.TabIndex = 18;
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1425, 798);
            this.Controls.Add(this.label_input_change);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label_stack);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.buttonTact);
            this.Controls.Add(this.label_postfix_symbol);
            this.Controls.Add(this.label_input);
            this.Controls.Add(this.radioButton2);
            this.Controls.Add(this.radioButton1);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.but_input);
            this.Name = "MainForm";
            this.Text = "Мультимедийная обучающая программа \"Трансляция алгeбраических выражений\"";
            this.Load += new System.EventHandler(this.Form1_Load);
            this.ResumeLayout(false);
        }

        private System.Windows.Forms.Label label_input_change;

        private System.Windows.Forms.Label label6;

        private System.Windows.Forms.Label label_stack;

        private System.Windows.Forms.Button but_input;

        private System.Windows.Forms.Button buttonTact;
        private System.Windows.Forms.Button button3;

        private System.Windows.Forms.Label label_postfix_symbol;

        private System.Windows.Forms.Label label_input;

        private System.Windows.Forms.RadioButton radioButton2;

        private System.Windows.Forms.RadioButton radioButton1;

        private System.Windows.Forms.Label label5;

        private System.Windows.Forms.Label label4;

        private System.Windows.Forms.Button Задать;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;

        #endregion
    }
}

