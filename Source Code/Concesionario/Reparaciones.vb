Imports System.Data.SqlClient

Public Class Reparaciones
    ' Propiedades
    Public idReparacion As Int32
    Dim dniMecanico, nombreMecanico, apellidoMecanico As String
    Dim fechaContratacion As DateTime
    Dim salario As Decimal
    Public idReparacionNuevo As Int32

    ' Al cargar el formulario ejecuta obtenerCoches y ordena la lista, en este caso por la matricula
    Private Sub Reparaciones_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        obtenerCoches()
        ListBox1.Sorted = True
        Label5.Visible = False
    End Sub

    ' Obtener coches de la BD e introducirla en la listbox
    Public Sub obtenerCoches()
        ListBox1.Items.Clear()
        ' Apertura de la conexion
        Form1.con.Open()
        ' Consultas
        Dim cmd As New SqlCommand("select matricula from coches", Form1.con)
        ' EJecucion de la consulta
        Using rdr As SqlDataReader = cmd.ExecuteReader()
            ' Mientras la consulta devuelva datos
            While rdr.Read()
                ' Obtencion de datos y guardado en memoria
                ListBox1.Items.Add(rdr.GetString(0).Trim)
            End While
            rdr.Close()
        End Using
        Form1.PictureBox1.Visible = False
        Form1.con.Close()
    End Sub

    ' Boton salir
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    ' Al cambiar de item en la listbox1
    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.SelectedItem IsNot Nothing Then
            obtenerReparacion()
            Label4.Visible = True
            Label5.Text = ListBox2.Items.Count
            Label5.Visible = True
        End If
    End Sub

    ' Obtener venta especifica de la BD
    Public Sub obtenerReparacion()
        ListBox2.Items.Clear()
        ListBox3.Items.Clear()
        ' Apertura de la conexion
        Form1.con.Open()
        ' Consultas
        Dim cmd As New SqlCommand("select idReparacion from fichaReparaciones where matricula = '" + ListBox1.SelectedItem.ToString.Trim + "'", Form1.con)
        ' EJecucion de la consulta
        Using rdr As SqlDataReader = cmd.ExecuteReader()
            ' Mientras la consulta devuelva datos
            While rdr.Read()
                ' Obtencion de datos y guardado en memoria
                ListBox2.Items.Add(rdr.GetInt32(0).ToString.Trim)
            End While
            rdr.Close()
        End Using
        Form1.con.Close()
    End Sub

    ' Boton eliminar
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        If ListBox2.SelectedItem IsNot Nothing Then
            Dim idReparacion As String = ListBox2.SelectedItem.ToString.Trim
            ' Apertura de la conexion
            Form1.con.Open()
            ' Consulta para borrar reparacion
            Dim cmd As New SqlCommand("delete from fichaReparaciones where idReparacion = " + idReparacion, Form1.con)
            ' EJecucion de la consulta
            cmd.ExecuteNonQuery()
            Form1.con.Close()
            obtenerReparacion()
            Label5.Text = ListBox2.Items.Count
        Else
            MsgBox("No se elegió ninguna reparación.")
        End If
    End Sub

    ' Boton agregar
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        ' Apertura de la conexion
        Form1.con.Open()
        ' Consulta para borrar reparacion
        Dim cmd As New SqlCommand("select max(idReparacion) from fichaReparaciones", Form1.con)
        ' EJecucion de la consulta
        Using rdr As SqlDataReader = cmd.ExecuteReader()
            ' Mientras la consulta devuelva datos
            While rdr.Read()
                ' Obtencion de idReparacion a agregar
                Try
                    idReparacionNuevo = rdr.GetInt32(0) + 1
                Catch ex As SqlTypes.SqlNullValueException
                    idReparacionNuevo = 1
                End Try
            End While
            rdr.Close()
        End Using
        Form1.con.Close()
        addReparacion.ShowDialog()
    End Sub

    ' Al cambio de item en la listbox2
    Private Sub ListBox2_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox2.SelectedIndexChanged
        obtenerMecanicos()
    End Sub

    ' Obtener todos los mecanicos de la reparacion elegida
    Public Sub obtenerMecanicos()
        ListBox3.Items.Clear()
        ' Apertura de la conexion
        Form1.con.Open()
        ' Consultas
        Dim cmd As New SqlCommand("select dniMecanico from datosReparacion where idReparacion = " + ListBox2.SelectedItem.ToString.Trim, Form1.con)
        ' EJecucion de la consulta
        Using rdr As SqlDataReader = cmd.ExecuteReader()
            ' Mientras la consulta devuelva datos
            While rdr.Read()
                ' Obtencion de datos y guardado en memoria
                ListBox3.Items.Add(rdr.GetString(0))
            End While
            rdr.Close()
        End Using
        Form1.con.Close()
    End Sub

    ' Boton ver mecanico
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        If ListBox3.SelectedItem IsNot Nothing Then
            obtenerDatoMecanico()
            Dim mecanico As DialogResult = MessageBox.Show("DNI: " + dniMecanico + vbLf + "Nombre: " + nombreMecanico + " " + apellidoMecanico + vbLf + "Fecha de contratación: " + fechaContratacion.ToString("yyyy-MM-dd").Trim + vbLf + "Salario: " + salario.ToString.Trim + "€", "Mecánico", MessageBoxButtons.OK)
        End If
    End Sub

    ' Obtener mecanico especifico de la BD
    Public Sub obtenerDatoMecanico()
        ' Apertura de la conexion
        Form1.con.Open()
        ' Consultas
        Dim cmd As New SqlCommand("select * from mecanicos where dni = '" + ListBox3.SelectedItem.ToString.Trim + "'", Form1.con)
        ' EJecucion de la consulta
        Using rdr As SqlDataReader = cmd.ExecuteReader()
            ' Mientras la consulta devuelva datos
            While rdr.Read()
                ' Obtencion de datos y guardado en memoria
                dniMecanico = rdr.GetString(0).Trim
                nombreMecanico = rdr.GetString(1).Trim
                apellidoMecanico = rdr.GetString(2).Trim
                fechaContratacion = rdr.GetDateTime(3)
                salario = rdr.GetDecimal(4)
            End While
            rdr.Close()
        End Using
        Form1.con.Close()
    End Sub
End Class