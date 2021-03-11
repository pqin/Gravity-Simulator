namespace GravitySimulator
{
    partial class Form1
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this._panelControls = new System.Windows.Forms.Panel();
            this.label1 = new System.Windows.Forms.Label();
            this._numMass = new System.Windows.Forms.NumericUpDown();
            this._btnClear = new System.Windows.Forms.Button();
            this._panelCanvas = new System.Windows.Forms.Panel();
            this._panelControls.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this._numMass)).BeginInit();
            this.SuspendLayout();
            // 
            // _panelControls
            // 
            this._panelControls.AutoScroll = true;
            this._panelControls.Controls.Add(this.label1);
            this._panelControls.Controls.Add(this._numMass);
            this._panelControls.Controls.Add(this._btnClear);
            this._panelControls.Dock = System.Windows.Forms.DockStyle.Left;
            this._panelControls.Location = new System.Drawing.Point(0, 0);
            this._panelControls.Name = "_panelControls";
            this._panelControls.Size = new System.Drawing.Size(200, 450);
            this._panelControls.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 64);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(32, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Mass";
            // 
            // _numMass
            // 
            this._numMass.Location = new System.Drawing.Point(12, 80);
            this._numMass.Maximum = new decimal(new int[] {
            1000000,
            0,
            0,
            0});
            this._numMass.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._numMass.Name = "_numMass";
            this._numMass.Size = new System.Drawing.Size(182, 20);
            this._numMass.TabIndex = 0;
            this._numMass.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // _btnClear
            // 
            this._btnClear.Location = new System.Drawing.Point(12, 12);
            this._btnClear.Name = "_btnClear";
            this._btnClear.Size = new System.Drawing.Size(182, 42);
            this._btnClear.TabIndex = 0;
            this._btnClear.Text = "Clear";
            this._btnClear.UseVisualStyleBackColor = true;
            // 
            // _panelCanvas
            // 
            this._panelCanvas.BackColor = System.Drawing.Color.Black;
            this._panelCanvas.Cursor = System.Windows.Forms.Cursors.Cross;
            this._panelCanvas.Dock = System.Windows.Forms.DockStyle.Fill;
            this._panelCanvas.Location = new System.Drawing.Point(200, 0);
            this._panelCanvas.Name = "_panelCanvas";
            this._panelCanvas.Size = new System.Drawing.Size(600, 450);
            this._panelCanvas.TabIndex = 1;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this._panelCanvas);
            this.Controls.Add(this._panelControls);
            this.Name = "Form1";
            this.Text = "Gravity Simulator";
            this._panelControls.ResumeLayout(false);
            this._panelControls.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this._numMass)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel _panelControls;
        private System.Windows.Forms.Panel _panelCanvas;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.NumericUpDown _numMass;
        private System.Windows.Forms.Button _btnClear;
    }
}

