namespace AffinTransform3D
{
    partial class Form1
    {
        /// <summary>
        /// Требуется переменная конструктора.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Освободить все используемые ресурсы.
        /// </summary>
        /// <param name="disposing">истинно, если управляемый ресурс должен быть удален; иначе ложно.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Код, автоматически созданный конструктором форм Windows

        /// <summary>
        /// Обязательный метод для поддержки конструктора - не изменяйте
        /// содержимое данного метода при помощи редактора кода.
        /// </summary>
        private void InitializeComponent()
        {
            this.icosahedron = new System.Windows.Forms.RadioButton();
            this.dodecahedron = new System.Windows.Forms.RadioButton();
            this.octahedron = new System.Windows.Forms.RadioButton();
            this.hexahedron = new System.Windows.Forms.RadioButton();
            this.tetrahedron = new System.Windows.Forms.RadioButton();
            this.displacement_button = new System.Windows.Forms.Button();
            this.z_shift = new System.Windows.Forms.NumericUpDown();
            this.y_shift = new System.Windows.Forms.NumericUpDown();
            this.label2 = new System.Windows.Forms.Label();
            this.x_shift = new System.Windows.Forms.NumericUpDown();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.rotate_button = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.z_rotate = new System.Windows.Forms.NumericUpDown();
            this.y_rotate = new System.Windows.Forms.NumericUpDown();
            this.x_rotate = new System.Windows.Forms.NumericUpDown();
            this.z_scale = new System.Windows.Forms.NumericUpDown();
            this.scale_button = new System.Windows.Forms.Button();
            this.y_scale = new System.Windows.Forms.NumericUpDown();
            this.label7 = new System.Windows.Forms.Label();
            this.x_scale = new System.Windows.Forms.NumericUpDown();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.cancel_button = new System.Windows.Forms.Button();
            this.yoz_reflect = new System.Windows.Forms.CheckBox();
            this.xoz_reflect = new System.Windows.Forms.CheckBox();
            this.xoy_reflect = new System.Windows.Forms.CheckBox();
            this.reflect_button = new System.Windows.Forms.Button();
            this.axis_angle = new System.Windows.Forms.NumericUpDown();
            this.label12 = new System.Windows.Forms.Label();
            this.axis_choice_button = new System.Windows.Forms.Button();
            this.axis_rotate_button = new System.Windows.Forms.Button();
            this.label10 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.z2_box = new System.Windows.Forms.NumericUpDown();
            this.z1_box = new System.Windows.Forms.NumericUpDown();
            this.axis_del_button = new System.Windows.Forms.Button();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.pictureBox = new System.Windows.Forms.PictureBox();
            this.label18 = new System.Windows.Forms.Label();
            this.y2_box = new System.Windows.Forms.NumericUpDown();
            this.x2_box = new System.Windows.Forms.NumericUpDown();
            this.label19 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.y1_box = new System.Windows.Forms.NumericUpDown();
            this.x1_box = new System.Windows.Forms.NumericUpDown();
            this.label21 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.z_shift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.y_shift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.x_shift)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.z_rotate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.y_rotate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.x_rotate)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.z_scale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.y_scale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.x_scale)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.axis_angle)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.z2_box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.z1_box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.y2_box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.x2_box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.y1_box)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.x1_box)).BeginInit();
            this.SuspendLayout();
            // 
            // icosahedron
            // 
            this.icosahedron.AutoSize = true;
            this.icosahedron.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.icosahedron.Location = new System.Drawing.Point(1281, 138);
            this.icosahedron.Margin = new System.Windows.Forms.Padding(4);
            this.icosahedron.Name = "icosahedron";
            this.icosahedron.Size = new System.Drawing.Size(92, 21);
            this.icosahedron.TabIndex = 4;
            this.icosahedron.TabStop = true;
            this.icosahedron.Text = "Икосаэдр";
            this.icosahedron.UseVisualStyleBackColor = true;
            this.icosahedron.CheckedChanged += new System.EventHandler(this.shape_CheckedChanged);
            // 
            // dodecahedron
            // 
            this.dodecahedron.AutoSize = true;
            this.dodecahedron.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.dodecahedron.Location = new System.Drawing.Point(1281, 107);
            this.dodecahedron.Margin = new System.Windows.Forms.Padding(4);
            this.dodecahedron.Name = "dodecahedron";
            this.dodecahedron.Size = new System.Drawing.Size(102, 21);
            this.dodecahedron.TabIndex = 3;
            this.dodecahedron.TabStop = true;
            this.dodecahedron.Text = "Додекаэдр";
            this.dodecahedron.UseVisualStyleBackColor = true;
            this.dodecahedron.CheckedChanged += new System.EventHandler(this.shape_CheckedChanged);
            // 
            // octahedron
            // 
            this.octahedron.AutoSize = true;
            this.octahedron.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.octahedron.Location = new System.Drawing.Point(1281, 76);
            this.octahedron.Margin = new System.Windows.Forms.Padding(4);
            this.octahedron.Name = "octahedron";
            this.octahedron.Size = new System.Drawing.Size(85, 21);
            this.octahedron.TabIndex = 2;
            this.octahedron.TabStop = true;
            this.octahedron.Text = "Октаэдр";
            this.octahedron.UseVisualStyleBackColor = true;
            this.octahedron.CheckedChanged += new System.EventHandler(this.shape_CheckedChanged);
            // 
            // hexahedron
            // 
            this.hexahedron.AutoSize = true;
            this.hexahedron.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.hexahedron.Location = new System.Drawing.Point(1281, 46);
            this.hexahedron.Margin = new System.Windows.Forms.Padding(4);
            this.hexahedron.Name = "hexahedron";
            this.hexahedron.Size = new System.Drawing.Size(90, 21);
            this.hexahedron.TabIndex = 1;
            this.hexahedron.TabStop = true;
            this.hexahedron.Text = "Гексаэдр";
            this.hexahedron.UseVisualStyleBackColor = true;
            this.hexahedron.CheckedChanged += new System.EventHandler(this.shape_CheckedChanged);
            // 
            // tetrahedron
            // 
            this.tetrahedron.AutoSize = true;
            this.tetrahedron.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.tetrahedron.Location = new System.Drawing.Point(1281, 15);
            this.tetrahedron.Margin = new System.Windows.Forms.Padding(4);
            this.tetrahedron.Name = "tetrahedron";
            this.tetrahedron.Size = new System.Drawing.Size(92, 21);
            this.tetrahedron.TabIndex = 0;
            this.tetrahedron.TabStop = true;
            this.tetrahedron.Text = "Тетраэдр";
            this.tetrahedron.UseVisualStyleBackColor = true;
            this.tetrahedron.CheckedChanged += new System.EventHandler(this.shape_CheckedChanged);
            // 
            // displacement_button
            // 
            this.displacement_button.Location = new System.Drawing.Point(1431, 138);
            this.displacement_button.Margin = new System.Windows.Forms.Padding(4);
            this.displacement_button.Name = "displacement_button";
            this.displacement_button.Size = new System.Drawing.Size(112, 31);
            this.displacement_button.TabIndex = 6;
            this.displacement_button.Text = "ОК";
            this.displacement_button.UseVisualStyleBackColor = true;
            this.displacement_button.Click += new System.EventHandler(this.displacement_button_Click);
            // 
            // z_shift
            // 
            this.z_shift.Location = new System.Drawing.Point(1457, 105);
            this.z_shift.Margin = new System.Windows.Forms.Padding(4);
            this.z_shift.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.z_shift.Name = "z_shift";
            this.z_shift.Size = new System.Drawing.Size(85, 22);
            this.z_shift.TabIndex = 5;
            // 
            // y_shift
            // 
            this.y_shift.Location = new System.Drawing.Point(1457, 73);
            this.y_shift.Margin = new System.Windows.Forms.Padding(4);
            this.y_shift.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.y_shift.Name = "y_shift";
            this.y_shift.Size = new System.Drawing.Size(85, 22);
            this.y_shift.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label2.Location = new System.Drawing.Point(1428, 80);
            this.label2.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(21, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Y:";
            // 
            // x_shift
            // 
            this.x_shift.Location = new System.Drawing.Point(1457, 43);
            this.x_shift.Margin = new System.Windows.Forms.Padding(4);
            this.x_shift.Minimum = new decimal(new int[] {
            100,
            0,
            0,
            -2147483648});
            this.x_shift.Name = "x_shift";
            this.x_shift.Size = new System.Drawing.Size(85, 22);
            this.x_shift.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label1.Location = new System.Drawing.Point(1427, 46);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(21, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "X:";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label3.Location = new System.Drawing.Point(1427, 107);
            this.label3.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(21, 17);
            this.label3.TabIndex = 2;
            this.label3.Text = "Z:";
            // 
            // rotate_button
            // 
            this.rotate_button.Location = new System.Drawing.Point(1281, 324);
            this.rotate_button.Margin = new System.Windows.Forms.Padding(4);
            this.rotate_button.Name = "rotate_button";
            this.rotate_button.Size = new System.Drawing.Size(120, 33);
            this.rotate_button.TabIndex = 10;
            this.rotate_button.Text = "ОК";
            this.rotate_button.UseVisualStyleBackColor = true;
            this.rotate_button.Click += new System.EventHandler(this.rotate_button_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label6.Location = new System.Drawing.Point(1277, 294);
            this.label6.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(29, 17);
            this.label6.TabIndex = 9;
            this.label6.Text = "0Z:";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label5.Location = new System.Drawing.Point(1277, 262);
            this.label5.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(29, 17);
            this.label5.TabIndex = 8;
            this.label5.Text = "0Y:";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F);
            this.label4.Location = new System.Drawing.Point(1277, 230);
            this.label4.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(29, 17);
            this.label4.TabIndex = 7;
            this.label4.Text = "0X:";
            // 
            // z_rotate
            // 
            this.z_rotate.Location = new System.Drawing.Point(1308, 292);
            this.z_rotate.Margin = new System.Windows.Forms.Padding(4);
            this.z_rotate.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.z_rotate.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.z_rotate.Name = "z_rotate";
            this.z_rotate.Size = new System.Drawing.Size(85, 22);
            this.z_rotate.TabIndex = 6;
            // 
            // y_rotate
            // 
            this.y_rotate.Location = new System.Drawing.Point(1308, 260);
            this.y_rotate.Margin = new System.Windows.Forms.Padding(4);
            this.y_rotate.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.y_rotate.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.y_rotate.Name = "y_rotate";
            this.y_rotate.Size = new System.Drawing.Size(85, 22);
            this.y_rotate.TabIndex = 5;
            // 
            // x_rotate
            // 
            this.x_rotate.Location = new System.Drawing.Point(1308, 228);
            this.x_rotate.Margin = new System.Windows.Forms.Padding(4);
            this.x_rotate.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.x_rotate.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.x_rotate.Name = "x_rotate";
            this.x_rotate.Size = new System.Drawing.Size(85, 22);
            this.x_rotate.TabIndex = 4;
            // 
            // z_scale
            // 
            this.z_scale.DecimalPlaces = 1;
            this.z_scale.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.z_scale.Location = new System.Drawing.Point(1316, 475);
            this.z_scale.Margin = new System.Windows.Forms.Padding(4);
            this.z_scale.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.z_scale.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.z_scale.Name = "z_scale";
            this.z_scale.Size = new System.Drawing.Size(85, 22);
            this.z_scale.TabIndex = 6;
            this.z_scale.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // scale_button
            // 
            this.scale_button.Location = new System.Drawing.Point(1292, 510);
            this.scale_button.Margin = new System.Windows.Forms.Padding(4);
            this.scale_button.Name = "scale_button";
            this.scale_button.Size = new System.Drawing.Size(109, 33);
            this.scale_button.TabIndex = 6;
            this.scale_button.Text = "ОК";
            this.scale_button.UseVisualStyleBackColor = true;
            this.scale_button.Click += new System.EventHandler(this.scale_button_Click);
            // 
            // y_scale
            // 
            this.y_scale.DecimalPlaces = 1;
            this.y_scale.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.y_scale.Location = new System.Drawing.Point(1316, 439);
            this.y_scale.Margin = new System.Windows.Forms.Padding(4);
            this.y_scale.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.y_scale.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.y_scale.Name = "y_scale";
            this.y_scale.Size = new System.Drawing.Size(85, 22);
            this.y_scale.TabIndex = 5;
            this.y_scale.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label7.Location = new System.Drawing.Point(1277, 448);
            this.label7.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(21, 17);
            this.label7.TabIndex = 1;
            this.label7.Text = "Y:";
            // 
            // x_scale
            // 
            this.x_scale.DecimalPlaces = 1;
            this.x_scale.Increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.x_scale.Location = new System.Drawing.Point(1316, 407);
            this.x_scale.Margin = new System.Windows.Forms.Padding(4);
            this.x_scale.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.x_scale.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.x_scale.Name = "x_scale";
            this.x_scale.Size = new System.Drawing.Size(85, 22);
            this.x_scale.TabIndex = 3;
            this.x_scale.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label8.Location = new System.Drawing.Point(1277, 416);
            this.label8.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(21, 17);
            this.label8.TabIndex = 0;
            this.label8.Text = "X:";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label9.Location = new System.Drawing.Point(1277, 478);
            this.label9.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(21, 17);
            this.label9.TabIndex = 2;
            this.label9.Text = "Z:";
            // 
            // cancel_button
            // 
            this.cancel_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.cancel_button.Location = new System.Drawing.Point(1432, 766);
            this.cancel_button.Margin = new System.Windows.Forms.Padding(4);
            this.cancel_button.Name = "cancel_button";
            this.cancel_button.Size = new System.Drawing.Size(120, 33);
            this.cancel_button.TabIndex = 5;
            this.cancel_button.Text = "Отмена";
            this.cancel_button.UseVisualStyleBackColor = true;
            this.cancel_button.Click += new System.EventHandler(this.cancel_button_Click);
            // 
            // yoz_reflect
            // 
            this.yoz_reflect.AutoSize = true;
            this.yoz_reflect.Location = new System.Drawing.Point(1431, 289);
            this.yoz_reflect.Margin = new System.Windows.Forms.Padding(4);
            this.yoz_reflect.Name = "yoz_reflect";
            this.yoz_reflect.Size = new System.Drawing.Size(59, 21);
            this.yoz_reflect.TabIndex = 13;
            this.yoz_reflect.Text = "YOZ";
            this.yoz_reflect.UseVisualStyleBackColor = true;
            // 
            // xoz_reflect
            // 
            this.xoz_reflect.AutoSize = true;
            this.xoz_reflect.Location = new System.Drawing.Point(1431, 257);
            this.xoz_reflect.Margin = new System.Windows.Forms.Padding(4);
            this.xoz_reflect.Name = "xoz_reflect";
            this.xoz_reflect.Size = new System.Drawing.Size(59, 21);
            this.xoz_reflect.TabIndex = 12;
            this.xoz_reflect.Text = "XOZ";
            this.xoz_reflect.UseVisualStyleBackColor = true;
            // 
            // xoy_reflect
            // 
            this.xoy_reflect.AutoSize = true;
            this.xoy_reflect.Location = new System.Drawing.Point(1431, 229);
            this.xoy_reflect.Margin = new System.Windows.Forms.Padding(4);
            this.xoy_reflect.Name = "xoy_reflect";
            this.xoy_reflect.Size = new System.Drawing.Size(59, 21);
            this.xoy_reflect.TabIndex = 11;
            this.xoy_reflect.Text = "XOY";
            this.xoy_reflect.UseVisualStyleBackColor = true;
            // 
            // reflect_button
            // 
            this.reflect_button.Location = new System.Drawing.Point(1431, 324);
            this.reflect_button.Margin = new System.Windows.Forms.Padding(4);
            this.reflect_button.Name = "reflect_button";
            this.reflect_button.Size = new System.Drawing.Size(112, 33);
            this.reflect_button.TabIndex = 10;
            this.reflect_button.Text = "ОК";
            this.reflect_button.UseVisualStyleBackColor = true;
            this.reflect_button.Click += new System.EventHandler(this.reflect_button_Click);
            // 
            // axis_angle
            // 
            this.axis_angle.Location = new System.Drawing.Point(1467, 592);
            this.axis_angle.Margin = new System.Windows.Forms.Padding(4);
            this.axis_angle.Maximum = new decimal(new int[] {
            360,
            0,
            0,
            0});
            this.axis_angle.Minimum = new decimal(new int[] {
            360,
            0,
            0,
            -2147483648});
            this.axis_angle.Name = "axis_angle";
            this.axis_angle.Size = new System.Drawing.Size(85, 22);
            this.axis_angle.TabIndex = 13;
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label12.Location = new System.Drawing.Point(1427, 593);
            this.label12.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(21, 17);
            this.label12.TabIndex = 12;
            this.label12.Text = "φ:";
            // 
            // axis_choice_button
            // 
            this.axis_choice_button.Location = new System.Drawing.Point(1430, 622);
            this.axis_choice_button.Margin = new System.Windows.Forms.Padding(4);
            this.axis_choice_button.Name = "axis_choice_button";
            this.axis_choice_button.Size = new System.Drawing.Size(112, 33);
            this.axis_choice_button.TabIndex = 11;
            this.axis_choice_button.Text = "Выбрать ось";
            this.axis_choice_button.UseVisualStyleBackColor = true;
            this.axis_choice_button.Click += new System.EventHandler(this.axis_choice_button_Click);
            // 
            // axis_rotate_button
            // 
            this.axis_rotate_button.Location = new System.Drawing.Point(1430, 704);
            this.axis_rotate_button.Margin = new System.Windows.Forms.Padding(4);
            this.axis_rotate_button.Name = "axis_rotate_button";
            this.axis_rotate_button.Size = new System.Drawing.Size(112, 33);
            this.axis_rotate_button.TabIndex = 10;
            this.axis_rotate_button.Text = "ОК";
            this.axis_rotate_button.UseVisualStyleBackColor = true;
            this.axis_rotate_button.Click += new System.EventHandler(this.axis_rotate_button_Click);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label10.Location = new System.Drawing.Point(1427, 563);
            this.label10.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(29, 17);
            this.label10.TabIndex = 9;
            this.label10.Text = "Z2:";
            this.label10.Click += new System.EventHandler(this.label10_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label11.Location = new System.Drawing.Point(1427, 476);
            this.label11.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(29, 17);
            this.label11.TabIndex = 8;
            this.label11.Text = "Z1:";
            // 
            // z2_box
            // 
            this.z2_box.Location = new System.Drawing.Point(1467, 562);
            this.z2_box.Margin = new System.Windows.Forms.Padding(4);
            this.z2_box.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.z2_box.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.z2_box.Name = "z2_box";
            this.z2_box.Size = new System.Drawing.Size(85, 22);
            this.z2_box.TabIndex = 6;
            // 
            // z1_box
            // 
            this.z1_box.Location = new System.Drawing.Point(1467, 473);
            this.z1_box.Margin = new System.Windows.Forms.Padding(4);
            this.z1_box.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.z1_box.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.z1_box.Name = "z1_box";
            this.z1_box.Size = new System.Drawing.Size(85, 22);
            this.z1_box.TabIndex = 5;
            // 
            // axis_del_button
            // 
            this.axis_del_button.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.axis_del_button.Location = new System.Drawing.Point(1430, 663);
            this.axis_del_button.Margin = new System.Windows.Forms.Padding(4);
            this.axis_del_button.Name = "axis_del_button";
            this.axis_del_button.Size = new System.Drawing.Size(112, 33);
            this.axis_del_button.TabIndex = 8;
            this.axis_del_button.Text = "Удалить ось";
            this.axis_del_button.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(1277, 202);
            this.label13.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(68, 17);
            this.label13.TabIndex = 11;
            this.label13.Text = "Поворот:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(1277, 384);
            this.label14.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(135, 17);
            this.label14.TabIndex = 12;
            this.label14.Text = "Масштабирование:";
            // 
            // label15
            // 
            this.label15.AutoSize = true;
            this.label15.Location = new System.Drawing.Point(1427, 15);
            this.label15.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(69, 17);
            this.label15.TabIndex = 13;
            this.label15.Text = "Перенос:";
            // 
            // label16
            // 
            this.label16.AutoSize = true;
            this.label16.Location = new System.Drawing.Point(1427, 202);
            this.label16.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(87, 17);
            this.label16.TabIndex = 14;
            this.label16.Text = "Отражение:";
            // 
            // label17
            // 
            this.label17.AutoSize = true;
            this.label17.Location = new System.Drawing.Point(1427, 384);
            this.label17.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(141, 17);
            this.label17.TabIndex = 15;
            this.label17.Text = "Поворот вокруг оси:";
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.FileName = "openFileDialog1";
            // 
            // pictureBox
            // 
            this.pictureBox.Location = new System.Drawing.Point(16, 15);
            this.pictureBox.Margin = new System.Windows.Forms.Padding(4);
            this.pictureBox.Name = "pictureBox";
            this.pictureBox.Size = new System.Drawing.Size(1253, 784);
            this.pictureBox.TabIndex = 16;
            this.pictureBox.TabStop = false;
            // 
            // label18
            // 
            this.label18.AutoSize = true;
            this.label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label18.Location = new System.Drawing.Point(1427, 535);
            this.label18.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(29, 17);
            this.label18.TabIndex = 17;
            this.label18.Text = "Y2:";
            // 
            // y2_box
            // 
            this.y2_box.Location = new System.Drawing.Point(1467, 534);
            this.y2_box.Margin = new System.Windows.Forms.Padding(4);
            this.y2_box.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.y2_box.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.y2_box.Name = "y2_box";
            this.y2_box.Size = new System.Drawing.Size(85, 22);
            this.y2_box.TabIndex = 18;
            // 
            // x2_box
            // 
            this.x2_box.Location = new System.Drawing.Point(1467, 504);
            this.x2_box.Margin = new System.Windows.Forms.Padding(4);
            this.x2_box.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.x2_box.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.x2_box.Name = "x2_box";
            this.x2_box.Size = new System.Drawing.Size(85, 22);
            this.x2_box.TabIndex = 19;
            // 
            // label19
            // 
            this.label19.AutoSize = true;
            this.label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label19.Location = new System.Drawing.Point(1427, 510);
            this.label19.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(29, 17);
            this.label19.TabIndex = 20;
            this.label19.Text = "X2:";
            // 
            // label20
            // 
            this.label20.AutoSize = true;
            this.label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label20.Location = new System.Drawing.Point(1427, 448);
            this.label20.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(29, 17);
            this.label20.TabIndex = 21;
            this.label20.Text = "Y1:";
            // 
            // y1_box
            // 
            this.y1_box.Location = new System.Drawing.Point(1467, 443);
            this.y1_box.Margin = new System.Windows.Forms.Padding(4);
            this.y1_box.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.y1_box.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.y1_box.Name = "y1_box";
            this.y1_box.Size = new System.Drawing.Size(85, 22);
            this.y1_box.TabIndex = 22;
            // 
            // x1_box
            // 
            this.x1_box.Location = new System.Drawing.Point(1467, 416);
            this.x1_box.Margin = new System.Windows.Forms.Padding(4);
            this.x1_box.Maximum = new decimal(new int[] {
            1000,
            0,
            0,
            0});
            this.x1_box.Minimum = new decimal(new int[] {
            1000,
            0,
            0,
            -2147483648});
            this.x1_box.Name = "x1_box";
            this.x1_box.Size = new System.Drawing.Size(85, 22);
            this.x1_box.TabIndex = 23;
            // 
            // label21
            // 
            this.label21.AutoSize = true;
            this.label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(204)));
            this.label21.Location = new System.Drawing.Point(1427, 417);
            this.label21.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(29, 17);
            this.label21.TabIndex = 24;
            this.label21.Text = "X1:";
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1586, 814);
            this.Controls.Add(this.label21);
            this.Controls.Add(this.x1_box);
            this.Controls.Add(this.y1_box);
            this.Controls.Add(this.label20);
            this.Controls.Add(this.label19);
            this.Controls.Add(this.x2_box);
            this.Controls.Add(this.y2_box);
            this.Controls.Add(this.label18);
            this.Controls.Add(this.pictureBox);
            this.Controls.Add(this.label17);
            this.Controls.Add(this.axis_angle);
            this.Controls.Add(this.label16);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.yoz_reflect);
            this.Controls.Add(this.axis_choice_button);
            this.Controls.Add(this.axis_rotate_button);
            this.Controls.Add(this.label15);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.xoz_reflect);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.displacement_button);
            this.Controls.Add(this.z2_box);
            this.Controls.Add(this.xoy_reflect);
            this.Controls.Add(this.z1_box);
            this.Controls.Add(this.reflect_button);
            this.Controls.Add(this.label14);
            this.Controls.Add(this.z_shift);
            this.Controls.Add(this.z_scale);
            this.Controls.Add(this.y_shift);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.scale_button);
            this.Controls.Add(this.x_shift);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.rotate_button);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.y_scale);
            this.Controls.Add(this.icosahedron);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.x_scale);
            this.Controls.Add(this.axis_del_button);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.dodecahedron);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.z_rotate);
            this.Controls.Add(this.y_rotate);
            this.Controls.Add(this.octahedron);
            this.Controls.Add(this.x_rotate);
            this.Controls.Add(this.hexahedron);
            this.Controls.Add(this.cancel_button);
            this.Controls.Add(this.tetrahedron);
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "Form1";
            this.Text = "3D векторная графика";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.z_shift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.y_shift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.x_shift)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.z_rotate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.y_rotate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.x_rotate)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.z_scale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.y_scale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.x_scale)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.axis_angle)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.z2_box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.z1_box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.y2_box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.x2_box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.y1_box)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.x1_box)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.RadioButton tetrahedron;
        private System.Windows.Forms.RadioButton hexahedron;
        private System.Windows.Forms.RadioButton octahedron;
        private System.Windows.Forms.RadioButton dodecahedron;
        private System.Windows.Forms.RadioButton icosahedron;
        private System.Windows.Forms.Button displacement_button;
        private System.Windows.Forms.NumericUpDown z_shift;
        private System.Windows.Forms.NumericUpDown y_shift;
        private System.Windows.Forms.NumericUpDown x_shift;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button rotate_button;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown z_rotate;
        private System.Windows.Forms.NumericUpDown y_rotate;
        private System.Windows.Forms.NumericUpDown x_rotate;
        private System.Windows.Forms.Button scale_button;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.NumericUpDown x_scale;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.NumericUpDown z_scale;
        private System.Windows.Forms.NumericUpDown y_scale;
        private System.Windows.Forms.Button cancel_button;
        private System.Windows.Forms.Button reflect_button;
        private System.Windows.Forms.CheckBox yoz_reflect;
        private System.Windows.Forms.CheckBox xoz_reflect;
        private System.Windows.Forms.CheckBox xoy_reflect;
        private System.Windows.Forms.Button axis_rotate_button;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.NumericUpDown z2_box;
        private System.Windows.Forms.NumericUpDown z1_box;
        private System.Windows.Forms.Button axis_choice_button;
        private System.Windows.Forms.Button axis_del_button;
        private System.Windows.Forms.NumericUpDown axis_angle;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.Windows.Forms.PictureBox pictureBox;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.NumericUpDown y2_box;
        private System.Windows.Forms.NumericUpDown x2_box;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.NumericUpDown y1_box;
        private System.Windows.Forms.NumericUpDown x1_box;
        private System.Windows.Forms.Label label21;
    }
}

