Imports System.Data.SqlClient

Public Class Ventas
    Public dniCliente As String

    ' Al cargar el formulario ejecuta obtenerCoches y ordena la lista, en este caso por la matricula
    Private Sub Ventas_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        obtenerCoches()
        ListBox1.Sorted = True
    End Sub

    ' Obtener coches de la BD e introducirla en la listbox
    Public Sub obtenerCoches()
        ListBox1.Items.Clear()
        ' Apertura de la conexion
        Form1.con.Open()
        ' Consultas
        Dim cmd As New SqlCommand("select matricula from fichaVentas", Form1.con)
        ' EJecucion de la consulta
        Using rdr As SqlDataReader = cmd.ExecuteReader()
            ' Mientras la consulta devuelva datos
            While rdr.Read()
                ' Obtencion de datos y guardado en memoria
                ListBox1.Items.Add(rdr.GetString(0))
            End While
            rdr.Close()
        End Using
        Form1.PictureBox1.Visible = False
        Form1.con.Close()
    End Sub

    ' Boton salir
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        labels(False)
        Me.Close()
    End Sub

    ' Al seleccionar item de listbox1
    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.SelectedItem IsNot Nothing Then
            labels(True)
            obtenerVenta()
        End If
    End Sub

    ' Obtener venta especifica de la BD
    Public Sub obtenerVenta()
        ' Creacion de la conexion
        Dim con As New SqlConnection("data source=HP-OMEN-GUILLER;initial catalog=concesionario;integrated security=true;persist security info=True;")
        ' Apertura de la conexion
        con.Open()
        ' Consultas
        Dim cmd As New SqlCommand("select dniCliente from fichaVentas where matricula = '" + ListBox1.SelectedItem.ToString.Trim + "'", con)
        ' EJecucion de la consulta
        Using rdr As SqlDataReader = cmd.ExecuteReader()
            ' Mientras la consulta devuelva datos
            While rdr.Read()
                ' Obtencion de datos y guardado en memoria
                dniCliente = rdr.GetString(0).Trim
            End While
            rdr.Close()
        End Using
        con.Close()
        obtenerCoche()
        obtenerClientes()
    End Sub

    ' Obtener clientes de la BD
    Public Sub obtenerClientes()
        ' Apertura de la conexion
        Form1.con.Open()
        ' Consultas
        Dim cmd As New SqlCommand("select nombre, apellidos, telefono from cliente where dni = '" + dniCliente + "'", Form1.con)
        ' EJecucion de la consulta
        Using rdr As SqlDataReader = cmd.ExecuteReader()
            ' Mientras la consulta devuelva datos
            While rdr.Read()
                ' Obtencion de datos y guardado en memoria
                Label12.Text = dniCliente
                Label13.Text = rdr.GetString(0).Trim + " " + rdr.GetString(1)
                Label14.Text = rdr.GetInt32(2).ToString
            End While
            rdr.Close()
        End Using
        Form1.con.Close()
    End Sub

    ' Obtener clientes de la BD
    Public Sub obtenerCoche()
        ' Apertura de la conexion
        Form1.con.Open()
        ' Consultas
        Dim cmd As New SqlCommand("select marca, modelo from coches where matricula = '" + ListBox1.SelectedItem.ToString.Trim + "'", Form1.con)
        ' EJecucion de la consulta
        Using rdr As SqlDataReader = cmd.ExecuteReader()
            ' Mientras la consulta devuelva datos
            While rdr.Read()
                ' Obtencion de datos y guardado en memoria
                Label9.Text = ListBox1.SelectedItem.ToString.Trim
                Label10.Text = rdr.GetString(0)
                Label11.Text = rdr.GetString(1)
            End While
            rdr.Close()
        End Using
        Form1.con.Close()
    End Sub

    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If ListBox1.SelectedItem IsNot Nothing Then
            Dim matricula As String = ListBox1.SelectedItem.ToString.Trim
            ' Apertura de la conexion
            Form1.con.Open()
            ' Consultas
            Dim cmd As New SqlCommand("delete from fichaVentas where matricula = '" + matricula + "'", Form1.con)
            ' EJecucion de la consulta
            cmd.ExecuteNonQuery()
            Form1.con.Close()
            ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
            labels(False)
        Else
            MsgBox("No se elegió ningún coche.")
        End If
        labels(False)
    End Sub

    Private Sub labels(booleano As Boolean)
        Label1.Visible = booleano
        Label2.Visible = booleano
        Label3.Visible = booleano
        Label4.Visible = booleano
        Label5.Visible = booleano
        Label6.Visible = booleano
        Label7.Visible = booleano
        Label8.Visible = booleano

        Label9.Visible = booleano
        Label10.Visible = booleano
        Label11.Visible = booleano
        Label12.Visible = booleano
        Label13.Visible = booleano
        Label14.Visible = booleano
    End Sub

    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        addVenta.ShowDialog()
    End Sub
End Class