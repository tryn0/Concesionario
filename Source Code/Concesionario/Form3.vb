Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Public Class Form3
    ' Diccionario con los datos de los clientes
    Public clientes As New Dictionary(Of String, List(Of String))

    ' Boton agregar
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        addCustomer.ShowDialog()
    End Sub

    ' Boton salir
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        clientes.Clear()
        hide()
        Me.Close()
    End Sub

    ' Al cargar el formulario ejecuta obtenerCoches y ordena la lista, en este caso por la matricula
    Private Sub Form3_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        obtenerClientes()
        ListBox1.Sorted = True
    End Sub

    ' Obtener clientes de la BD e introducirla en la listbox
    Public Sub obtenerClientes()
        clientes.Clear()
        ListBox1.Items.Clear()
        ' Apertura de la conexion
        Form1.con.Open()
        ' Consultas
        Dim cmd As New SqlCommand("select * from cliente", Form1.con)
        ' EJecucion de la consulta
        Using rdr As SqlDataReader = cmd.ExecuteReader()
            ' Mientras la consulta devuelva datos
            While rdr.Read()
                ' Obtencion de datos y guardado en memoria
                Dim info As New List(Of String) From {rdr.GetString(0), rdr.GetString(1), rdr.GetString(2), rdr.GetString(3), rdr.GetInt32(4).ToString()}
                clientes.Add(rdr.GetString(0), info)
            End While
            rdr.Close()
        End Using
        Form1.con.Close()

        ' Iteracion sobre los trabajadores para agregarlos al listbox1
        For Each cliente As KeyValuePair(Of String, List(Of String)) In clientes
            ListBox1.Items.Add(cliente.Value(0))
            If cliente.Value(0) = clientes.Last().Value(0) Then
                Form1.PictureBox1.Visible = False
            End If
        Next
    End Sub

    ' Al seleccionar elementos de la listbox obtiene datos del coche y es introducida en sus respectivos controles
    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        clienteSeleccionado(getCliente())
    End Sub

    ' Devuelve la lista de datos del coche seleccionado en el listbox
    Private Function getCliente()
        If ListBox1.SelectedItem IsNot Nothing Then
            Dim dni As String = ListBox1.SelectedItem
            For Each cliente As KeyValuePair(Of String, List(Of String)) In clientes
                If cliente.Value(0).Trim() = dni.Trim() Then
                    Return cliente.Value
                End If
            Next
        End If
    End Function

    ' Ocultar controles
    Private Sub hide()
        Label1.Visible = False
        Label2.Visible = False
        Label3.Visible = False
        Label4.Visible = False
        Label5.Visible = False

        TextBox1.Visible = False
        TextBox2.Visible = False
        TextBox3.Visible = False
        TextBox4.Visible = False
        TextBox5.Visible = False

        Button4.Visible = False
    End Sub

    ' Mostrar controles
    Private Sub unhide()
        Label1.Visible = True
        Label2.Visible = True
        Label2.Visible = True
        Label3.Visible = True
        Label4.Visible = True
        Label5.Visible = True

        TextBox1.Visible = True
        TextBox2.Visible = True
        TextBox3.Visible = True
        TextBox4.Visible = True
        TextBox5.Visible = True

        Button4.Visible = True
    End Sub

    ' Muestra/oculta controles dependiendo si es coche nuevo o usado
    Private Sub clienteSeleccionado(cliente As List(Of String))
        If cliente IsNot Nothing Then
            TextBox1.Text = cliente(0).Trim
            TextBox2.Text = cliente(1).Trim
            TextBox3.Text = cliente(2).Trim
            TextBox4.Text = cliente(3).Trim
            TextBox5.Text = cliente(4).Trim
            unhide()
        End If
    End Sub

    ' Guardar cambios del cliente en la BD
    Private Function cambios(dni As String)
        If TextBox2.Text IsNot "" And TextBox3.Text IsNot "" And TextBox4.Text IsNot "" And TextBox5.Text IsNot "" Then
            Dim regexTelefono As New Regex("^[6789]\d{8}$")
            ' Comprueba si el DNI es valido
            If regexTelefono.IsMatch(TextBox5.Text) Then
                Form1.con.Open()
                ' Update
                Dim cmd As New SqlCommand("update cliente set nombre = '" + TextBox2.Text.ToString.Trim() + "', apellidos = '" + TextBox3.Text.ToString.Trim() + "', direccion = '" + TextBox4.Text.ToString.Trim() + "', telefono = '" + TextBox5.Text.ToString.Trim() + "'  where dni = '" + dni.Trim() + "'", Form1.con)
                ' EJecucion de la consulta
                cmd.ExecuteNonQuery()
                Form1.con.Close()
                obtenerClientes()
            Else
                MsgBox("Introduzca un teléfono válido")
            End If
        Else
            MsgBox("Rellene todos los datos")
        End If
    End Function

    ' Boton guardar
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        cambios(getCliente(0))
    End Sub

    ' Boton y accion Eliminar
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ListBox1.SelectedItem IsNot Nothing Then
            ' Apertura de la conexion
            Form1.con.Open()
            ' Delete
            Dim cmd As New SqlCommand("delete from cliente where dni = '" + getCliente()(0) + "'", Form1.con)
            ' EJecucion de la consulta
            cmd.ExecuteNonQuery()
            Form1.con.Close()
            ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
            hide()
        Else
            MsgBox("No se elegió ningún clientes.")
        End If
    End Sub
End Class