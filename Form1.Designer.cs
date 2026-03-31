namespace AIChatbot
{
    partial class Form1
    {
        private System.ComponentModel.IContainer components = null;

        // Controls
        private System.Windows.Forms.RichTextBox rtbChat;
        private System.Windows.Forms.TextBox     txtInput;
        private System.Windows.Forms.Button      btnSend;
        private System.Windows.Forms.Button      btnClear;
        private System.Windows.Forms.Label       lblTitle;
        private System.Windows.Forms.Label       lblStatus;
        private System.Windows.Forms.Panel       pnlTop;
        private System.Windows.Forms.Panel       pnlBottom;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.rtbChat    = new System.Windows.Forms.RichTextBox();
            this.txtInput   = new System.Windows.Forms.TextBox();
            this.btnSend    = new System.Windows.Forms.Button();
            this.btnClear   = new System.Windows.Forms.Button();
            this.lblTitle   = new System.Windows.Forms.Label();
            this.lblStatus  = new System.Windows.Forms.Label();
            this.pnlTop     = new System.Windows.Forms.Panel();
            this.pnlBottom  = new System.Windows.Forms.Panel();

            // ── Form ──────────────────────────────────
            this.Text            = "AI Chatbot — Powered by Claude";
            this.Size            = new System.Drawing.Size(750, 600);
            this.MinimumSize     = new System.Drawing.Size(600, 450);
            this.BackColor       = System.Drawing.Color.FromArgb(245, 245, 250);
            this.Font            = new System.Drawing.Font("Segoe UI", 9f);
            this.StartPosition   = System.Windows.Forms.FormStartPosition.CenterScreen;

            // ── Top Panel (Title Bar) ─────────────────
            this.pnlTop.Dock      = System.Windows.Forms.DockStyle.Top;
            this.pnlTop.Height    = 55;
            this.pnlTop.BackColor = System.Drawing.Color.FromArgb(0, 80, 160);
            this.pnlTop.Padding   = new System.Windows.Forms.Padding(12, 0, 12, 0);

            this.lblTitle.Text      = "🤖  AI Chatbot  —  Powered by Claude";
            this.lblTitle.Dock      = System.Windows.Forms.DockStyle.Fill;
            this.lblTitle.ForeColor = System.Drawing.Color.White;
            this.lblTitle.Font      = new System.Drawing.Font("Segoe UI", 14f, System.Drawing.FontStyle.Bold);
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;

            this.pnlTop.Controls.Add(this.lblTitle);

            // ── Chat Display ──────────────────────────
            this.rtbChat.Dock           = System.Windows.Forms.DockStyle.Fill;
            this.rtbChat.ReadOnly       = true;
            this.rtbChat.BackColor      = System.Drawing.Color.White;
            this.rtbChat.BorderStyle    = System.Windows.Forms.BorderStyle.None;
            this.rtbChat.Font           = new System.Drawing.Font("Segoe UI", 9.5f);
            this.rtbChat.Padding        = new System.Windows.Forms.Padding(10);
            this.rtbChat.ScrollBars     = System.Windows.Forms.RichTextBoxScrollBars.Vertical;

            // ── Bottom Panel (Input Area) ─────────────
            this.pnlBottom.Dock        = System.Windows.Forms.DockStyle.Bottom;
            this.pnlBottom.Height      = 70;
            this.pnlBottom.BackColor   = System.Drawing.Color.FromArgb(235, 235, 242);
            this.pnlBottom.Padding     = new System.Windows.Forms.Padding(10, 10, 10, 10);

            // Input TextBox
            this.txtInput.Location    = new System.Drawing.Point(10, 18);
            this.txtInput.Height      = 34;
            this.txtInput.Font        = new System.Drawing.Font("Segoe UI", 10f);
            this.txtInput.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.txtInput.PlaceholderText = "Type your message here... (Enter to send)";
            this.txtInput.Anchor      = System.Windows.Forms.AnchorStyles.Left
                                      | System.Windows.Forms.AnchorStyles.Right
                                      | System.Windows.Forms.AnchorStyles.Top;
            this.txtInput.Width       = 490;
            this.txtInput.KeyDown    += new System.Windows.Forms.KeyEventHandler(this.txtInput_KeyDown);

            // Send Button
            this.btnSend.Text         = "Send  ▶";
            this.btnSend.Location     = new System.Drawing.Point(510, 15);
            this.btnSend.Size         = new System.Drawing.Size(100, 38);
            this.btnSend.BackColor    = System.Drawing.Color.FromArgb(0, 120, 215);
            this.btnSend.ForeColor    = System.Drawing.Color.White;
            this.btnSend.FlatStyle    = System.Windows.Forms.FlatStyle.Flat;
            this.btnSend.Font         = new System.Drawing.Font("Segoe UI", 9.5f, System.Drawing.FontStyle.Bold);
            this.btnSend.Cursor       = System.Windows.Forms.Cursors.Hand;
            this.btnSend.Anchor       = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top;
            this.btnSend.Click       += new System.EventHandler(this.btnSend_Click);

            // Clear Button
            this.btnClear.Text        = "Clear";
            this.btnClear.Location    = new System.Drawing.Point(618, 15);
            this.btnClear.Size        = new System.Drawing.Size(80, 38);
            this.btnClear.BackColor   = System.Drawing.Color.FromArgb(200, 50, 50);
            this.btnClear.ForeColor   = System.Drawing.Color.White;
            this.btnClear.FlatStyle   = System.Windows.Forms.FlatStyle.Flat;
            this.btnClear.Font        = new System.Drawing.Font("Segoe UI", 9.5f);
            this.btnClear.Cursor      = System.Windows.Forms.Cursors.Hand;
            this.btnClear.Anchor      = System.Windows.Forms.AnchorStyles.Right | System.Windows.Forms.AnchorStyles.Top;
            this.btnClear.Click      += new System.EventHandler(this.btnClear_Click);

            this.pnlBottom.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.txtInput, this.btnSend, this.btnClear
            });

            // ── Status Bar ────────────────────────────
            this.lblStatus.Dock      = System.Windows.Forms.DockStyle.Bottom;
            this.lblStatus.Height    = 22;
            this.lblStatus.Text      = "Ready";
            this.lblStatus.ForeColor = System.Drawing.Color.Green;
            this.lblStatus.Font      = new System.Drawing.Font("Segoe UI", 8.5f);
            this.lblStatus.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.lblStatus.Padding   = new System.Windows.Forms.Padding(0, 0, 12, 0);
            this.lblStatus.BackColor = System.Drawing.Color.FromArgb(220, 220, 228);

            // ── Add to Form ───────────────────────────
            this.Controls.AddRange(new System.Windows.Forms.Control[] {
                this.rtbChat, this.pnlTop, this.pnlBottom, this.lblStatus
            });

            this.components = new System.ComponentModel.Container();
            this.ResumeLayout(false);
        }
    }
}
