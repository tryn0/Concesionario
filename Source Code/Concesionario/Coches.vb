Imports System.Data.SqlClient

Public Class Coches
    Private Sub Coches_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        obtenerClientes()
        obtenerMecanicos()
    End Sub

    ' Obtener clientes de la BD
    Private Sub obtenerClientes()
        Label6.Visible = False
        Label7.Visible = False
        ' Apertura de la conexion
        Form1.con.Open()
        ' Consultas
        Dim cmd As New SqlCommand("select dni from cliente", Form1.con)
        ' EJecucion de la consulta
        Using rdr As SqlDataReader = cmd.ExecuteReader()
            ' Mientras la consulta devuelva datos
            While rdr.Read()
                ' Obtencion de datos y guardado en listbox
                ListBox1.Items.Add(rdr.GetString(0).Trim)
            End While
            rdr.Close()
        End Using
        Form1.PictureBox1.Visible = False
        Form1.con.Close()
    End Sub


    ' Obtener cliente especifico de la BD
    Private Sub obtenerCliente()
        ' Apertura de la conexion
        Form1.con.Open()
        ' Consultas
        Dim cmd As New SqlCommand("select nombre, apellidos from cliente where dni = '" + ListBox1.SelectedItem.ToString.Trim + "'", Form1.con)
        ' EJecucion de la consulta
        Using rdr As SqlDataReader = cmd.ExecuteReader()
            ' Mientras la consulta devuelva datos
            While rdr.Read()
                ' Obtencion de datos y guardado en listbox
                Label6.Text = rdr.GetString(0).Trim + " " + rdr.GetString(1)
            End While
            rdr.Close()
        End Using
        Form1.con.Close()

        Label6.Visible = True
        Label7.Visible = True
    End Sub

    ' Obtener mecanicos de la BD
    Private Sub obtenerMecanicos()
        ' Apertura de la conexion
        Form1.con.Open()
        ' Consultas
        Dim cmd As New SqlCommand("select dni from mecanicos", Form1.con)
        ' EJecucion de la consulta
        Using rdr As SqlDataReader = cmd.ExecuteReader()
            ' Mientras la consulta devuelva datos
            While rdr.Read()
                ' Obtencion de datos y guardado en listbox
                ListBox3.Items.Add(rdr.GetString(0).Trim)
            End While
            rdr.Close()
        End Using
        Form1.con.Close()
    End Sub

    ' Obtener coches de un cliente la BD
    Private Sub obtenerCoches()
        Label4.Visible = False
        Label5.Visible = False
        Label5.Text = ""
        ListBox2.Items.Clear()
        ' Apertura de la conexion
        Form1.con.Open()
        ' Consultas
        Dim cmd As New SqlCommand("select matricula from fichaVentas where dniCliente = '" + ListBox1.SelectedItem.ToString.Trim + "'", Form1.con)
        ' EJecucion de la consulta
        Using rdr As SqlDataReader = cmd.ExecuteReader()
            ' Mientras la consulta devuelva datos
            While rdr.Read()
                ' Obtencion de datos y guardado en listbox
                ListBox2.Items.Add(rdr.GetString(0).Trim)
            End While
            rdr.Close()
        End Using
        Form1.con.Close()
    End Sub

    ' Boton salir
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        ListBox1.Items.Clear()
        ListBox2.Items.Clear()
        ListBox3.Items.Clear()
        ListBox4.Items.Clear()
        Me.Close()
    End Sub

    ' Al seleccionar un item de listbox1
    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        obtenerCliente()
        obtenerCoches()
    End Sub

    ' Obtener coche especifico la BD
    Private Sub obtenerCoche(matricula As String)
        ' Apertura de la conexion
        Form1.con.Open()
        ' Consultas
        Dim cmd As New SqlCommand("select marca, modelo from coches where matricula = '" + matricula + "'", Form1.con)
        ' EJecucion de la consulta
        Using rdr As SqlDataReader = cmd.ExecuteReader()
            ' Mientras la consulta devuelva datos
            While rdr.Read()
                ' Obtencion de datos y guardado en listbox
                Label5.Text = rdr.GetString(0).Trim + " " + rdr.GetString(1).Trim
            End While
            rdr.Close()
        End Using
        Form1.con.Close()
        Label4.Visible = True
        Label5.Visible = True
    End Sub

    ' Obtener coches de los mecanicos la BD
    Private Sub obtenerCochesMecanico()
        ListBox4.Items.Clear()
        ' Apertura de la conexion
        Form1.con.Open()
        ' Consultas
        Dim cmd As New SqlCommand("select matricula from fichaReparaciones where idReparacion in (select idReparacion from datosReparacion where dniMecanico = '" + ListBox3.SelectedItem.ToString.Trim + "')", Form1.con)
        ' EJecucion de la consulta
        Using rdr As SqlDataReader = cmd.ExecuteReader()
            ' Mientras la consulta devuelva datos
            While rdr.Read()
                ' Obtencion de datos y guardado en listbox
                ListBox4.Items.Add(rdr.GetString(0).Trim)
            End While
            rdr.Close()
        End Using
        Form1.con.Close()
    End Sub

    ' Al seleccionar item de listbox2
    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged
        If ListBox2.SelectedItem IsNot Nothing Then
            obtenerCoche(ListBox2.SelectedItem.ToString.Trim)
        End If
    End Sub

    ' Al seleccionar item de listbo3
    Private Sub ListBox3_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox3.SelectedIndexChanged
        If ListBox3.SelectedItem IsNot Nothing Then
            obtenerCochesMecanico()
        End If
    End Sub

    ' Al seleccionar item de listbox4
    Private Sub ListBox4_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox4.SelectedIndexChanged
        If ListBox3.SelectedItem IsNot Nothing Then
            obtenerCoche(ListBox4.SelectedItem.ToString.Trim)
        End If
    End Sub
End Class