Imports System.Data.SqlClient

Public Class addReparacion
    ' Boton salir
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        salida()
    End Sub

    ' Al salir borra todo y lo oculta
    Private Sub salida()
        ListBox1.Items.Clear()
        ListBox2.Items.Clear()
        Label2.Visible = False
        Label3.Visible = False
        TextBox1.Visible = False
        TextBox1.Text = ""
        Label6.Text = ""
        Label7.Text = ""
        Label6.Visible = False
        Label7.Visible = False
        DateTimePicker1.Visible = False
        Button2.Enabled = False
        Me.Close()
    End Sub

    ' Al cargar el formulario
    Private Sub addReparacion_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Label6.Text = ""
        Label7.Text = ""
        Label6.Visible = False
        Label7.Visible = False
        ' Al cargar el formulario copio la lista de matriculas de coches del formulario Reparacions, para evitar otra conexion y acceso a la BD
        For Each matricula As String In Reparaciones.ListBox1.Items
            ListBox1.Items.Add(matricula)
        Next

        obtenerMecanicos()
    End Sub

    Private Sub obtenerMecanicos()
        ListBox2.Items.Clear()
        ' Apertura de la conexion
        Form1.con.Open()
        ' Consultas
        Dim cmd As New SqlCommand("select dni from mecanicos ", Form1.con)
        ' EJecucion de la consulta
        Using rdr As SqlDataReader = cmd.ExecuteReader()
            ' Mientras la consulta devuelva datos
            While rdr.Read()
                ' Obtencion de datos y guardado en memoria
                ListBox2.Items.Add(rdr.GetString(0))
            End While
            rdr.Close()
        End Using
        Form1.con.Close()
    End Sub

    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.SelectedItem IsNot Nothing Then
            addEnable()
            Label2.Visible = True
            Label3.Visible = True
            TextBox1.Visible = True
            DateTimePicker1.Visible = True
            ' Apertura de la conexion
            Form1.con.Open()
            ' Consultas
            Dim cmd As New SqlCommand("select marca, modelo from coches where matricula = '" + ListBox1.SelectedItem.ToString.Trim + "'", Form1.con)
            ' EJecucion de la consulta
            Using rdr As SqlDataReader = cmd.ExecuteReader()
                ' Mientras la consulta devuelva datos
                While rdr.Read()
                    ' Obtencion de datos y guardado en memoria
                    Label7.Text = rdr.GetString(0).Trim + " " + rdr.GetString(1).Trim
                End While
                rdr.Close()
            End Using
            Form1.con.Close()
            Label7.Visible = True
        Else
            Label7.Visible = False
            Label7.Text = ""
            Label2.Visible = False
            Label3.Visible = False
            TextBox1.Visible = False
            TextBox1.Text = ""
            DateTimePicker1.Visible = False
        End If
    End Sub

    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged
        If ListBox2.SelectedItem IsNot Nothing Then
            addEnable()
            ' Apertura de la conexion
            Form1.con.Open()
            ' Consultas
            Dim cmd As New SqlCommand("select nombre, apellidos from mecanicos where dni = '" + ListBox2.SelectedItem.ToString.Trim + "'", Form1.con)
            ' EJecucion de la consulta
            Using rdr As SqlDataReader = cmd.ExecuteReader()
                ' Mientras la consulta devuelva datos
                While rdr.Read()
                    ' Obtencion de datos y guardado en memoria
                    Label6.Text = rdr.GetString(0).Trim + " " + rdr.GetString(1).Trim
                End While
                rdr.Close()
            End Using
            Form1.con.Close()
            Label6.Visible = True
        Else
            Label6.Visible = False
            Label6.Text = ""
        End If
    End Sub

    ' Habilitar boton agregar
    Private Sub addEnable()
        ' Si se ha introducido todos los datos necesarios para agregar una reparacion activa el boton agregar
        If ListBox1.SelectedItem IsNot Nothing And TextBox1.Value > 0 And ListBox2.SelectedItem IsNot Nothing And DateTimePicker1.Value.ToString.Length > 0 Then
            Button2.Enabled = True
        End If
    End Sub

    ' Al quitar focus de listbox2
    Private Sub ListBox2_LostFocus(sender As Object, e As EventArgs) Handles ListBox2.LostFocus
        addEnable()
    End Sub

    ' Al quitar focus de listbox1
    Private Sub ListBox1_LostFocus(sender As Object, e As EventArgs) Handles ListBox1.LostFocus
        addEnable()
    End Sub

    ' Al quitar focus de textbox1
    Private Sub TextBox1_LostFocus(sender As Object, e As EventArgs)
        addEnable()
    End Sub

    ' Al quitar focus de datetimepicker1
    Private Sub DateTimePicker1_LostFocus(sender As Object, e As EventArgs) Handles DateTimePicker1.LostFocus
        addEnable()
    End Sub

    Private Sub controlHoras()
        If TextBox1.Text > 500 Then
            MsgBox("La reparación no puede superar las 500h")
        End If
    End Sub

    Private Sub TextBox1_TextChanged(sender As Object, e As EventArgs)
        controlHoras()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.Text < 501 Then
            ' Apertura de la conexion
            Form1.con.Open()
            ' Consultas
            Dim cmd As New SqlCommand("insert into fichaReparaciones (matricula, horas, idReparacion, fecha) values('" + ListBox1.SelectedItem.ToString.Trim + "', " + TextBox1.Value.ToString.Trim + ", " + Reparaciones.idReparacionNuevo.ToString.Trim + ", '" + DateTimePicker1.Value.ToString("yyyy-MM-dd") + "')", Form1.con)
            ' EJecucion de la consulta
            cmd.ExecuteNonQuery()

            ' Introduccion de cada mecanico de la reparacion en datosReparacion
            For Each item In ListBox2.SelectedItems
                Dim cmd2 As New SqlCommand("insert into datosReparacion (idReparacion, dniMecanico) values(" + Reparaciones.idReparacionNuevo.ToString.Trim + ", '" + item + "')", Form1.con)
                ' EJecucion de la consulta
                cmd2.ExecuteNonQuery()
            Next

            Form1.con.Close()
            Reparaciones.ListBox2.Items.Clear()
            Reparaciones.ListBox3.Items.Clear()
            Reparaciones.Label5.Text = ""
            Reparaciones.Label5.Visible = False
            Reparaciones.Label4.Visible = False
            Reparaciones.obtenerCoches()
            salida()
        End If
    End Sub
End Class