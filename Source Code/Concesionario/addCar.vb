Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Public Class addCar
    ' Al cargar el formulario
    Private Sub addCar_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        ComboBox1.Items.Add("Sí")
        ComboBox1.Items.Add("No")
    End Sub

    ' Al seleccionar item del combobox1
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedItem = "Sí" Then
            Label7.Visible = False
            TextBox7.Visible = False
            TextBox7.Text = ""
            Label5.Visible = True
            TextBox5.Visible = True
            Label6.Visible = True
            DateTimePicker1.Visible = True
        ElseIf ComboBox1.SelectedItem = "No" Then
            Label7.Visible = True
            TextBox7.Visible = True
            Label5.Visible = False
            TextBox5.Visible = False
            TextBox5.Text = ""
            Label6.Visible = False
            DateTimePicker1.Visible = False
            DateTimePicker1.Text = ""
        End If
    End Sub

    ' Boton salir
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    ' Boton agregar
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.TextLength > 0 And TextBox2.TextLength > 0 And TextBox3.TextLength > 0 And TextBox4.TextLength > 0 Then
            If ComboBox1.SelectedItem = "Sí" And TextBox5.TextLength > 0 Then
                guardarCoche()
            ElseIf ComboBox1.SelectedItem = "No" And TextBox7.TextLength > 0 Then
                guardarCoche()
            Else
                MsgBox("Rellene el formulario completo para agregar un coche")
            End If
        Else
            MsgBox("Rellene el formulario completo para agregar un coche")
        End If
    End Sub

    ' Funcion para guardar el coche en la BD
    Private Sub guardarCoche()
        If Form2.coches.Keys.Contains(TextBox1.Text.Trim()) Then
            MsgBox("Ya existe un coche con esa matrícula")
        Else
            Dim regexMatricula As New Regex("^[0-9]{4,4}(?!.*(LL|CH))[BCDFGHJKLMNPRSTVWXYZ]{3}$")
            If regexMatricula.IsMatch(TextBox1.Text.Trim.ToUpper) Then
                ' Apertura de la conexion
                Form1.con.Open()
                ' Consultas, según si es usado o no
                Dim cmd As New SqlCommand
                If ComboBox1.SelectedItem = "Sí" And TextBox5.Text IsNot Nothing And DateTimePicker1.ToString.Length > 0 Then
                    cmd = New SqlCommand("INSERT INTO coches (matricula, usado, modelo, marca, color, kilometros, anioMatriculacion, numeroUnidades) VALUES('" + TextBox1.Text.Trim.ToUpper + "', 1, '" + TextBox3.Text.Trim() + "', '" + TextBox2.Text.Trim() + "', '" + TextBox4.Text.Trim() + "', " + TextBox5.Text.Trim() + ", '" + DateTimePicker1.Value.ToString("yyyy-MM-dd") + "', 0)", Form1.con)
                ElseIf ComboBox1.SelectedItem = "No" And TextBox7.TextLength > 0 Then
                    Dim ahora As String = DateTime.Now.ToString("yyyy-MM-dd")
                    cmd = New SqlCommand("INSERT INTO coches (matricula, usado, modelo, marca, color, kilometros, anioMatriculacion, numeroUnidades) VALUES('" + TextBox1.Text.Trim.ToUpper + "', 0, '" + TextBox3.Text.Trim() + "', '" + TextBox2.Text.Trim() + "', '" + TextBox4.Text.Trim() + "', 0, '" + ahora + "', " + TextBox7.Text.Trim() + ")", Form1.con)
                Else
                    MsgBox("Rellene el formulario completo para agregar un coche")
                End If
                ' EJecucion de la consulta
                cmd.ExecuteNonQuery()
                Form1.con.Close()
                Form2.obtenerCoches()
                Me.Close()
            Else
                MsgBox("Introduzca una mátricula válida")
            End If
        End If
    End Sub
End Class