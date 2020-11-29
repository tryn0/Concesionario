Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Public Class Form4
    ' Diccionario con los datos de los clientes
    Public mecanicos As New Dictionary(Of String, List(Of String))

    ' Boton agregar
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        addMecanico.ShowDialog()
    End Sub

    ' Boton salir
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        mecanicos.Clear()
        hide()
        Me.Close()
    End Sub

    ' Al cargar el formulario ejecuta obtenerCoches y ordena la lista, en este caso por la matricula
    Private Sub Form4_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        obtenerMecanicos()
        ListBox1.Sorted = True
    End Sub

    ' Obtener coches de la BD e introducirla en la listbox
    Public Sub obtenerMecanicos()
        mecanicos.Clear()
        ListBox1.Items.Clear()
        ' Apertura de la conexion
        Form1.con.Open()
        ' Consultas
        Dim cmd As New SqlCommand("select * from mecanicos", Form1.con)
        ' EJecucion de la consulta
        Using rdr As SqlDataReader = cmd.ExecuteReader()
            ' Mientras la consulta devuelva datos
            While rdr.Read()
                ' Obtencion de datos y guardado en memoria
                Dim info As New List(Of String) From {rdr.GetString(0), rdr.GetString(1), rdr.GetString(2), rdr.GetDateTime(3).ToString(), rdr.GetDecimal(4).ToString()}
                mecanicos.Add(rdr.GetString(0), info)
            End While
            rdr.Close()
        End Using
        Form1.con.Close()

        ' Iteracion sobre los trabajadores para agregarlos al listbox1
        For Each mecanico As KeyValuePair(Of String, List(Of String)) In mecanicos
            ListBox1.Items.Add(mecanico.Value(0))
            If mecanico.Value(0) = mecanicos.Last().Value(0) Then
                Form1.PictureBox1.Visible = False
            End If
        Next
    End Sub

    ' Al seleccionar elementos de la listbox obtiene datos del coche y es introducida en sus respectivos controles
    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        If ListBox1.SelectedItem IsNot Nothing Then
            mecanicoSeleccionado(mecanicos.Item(ListBox1.SelectedItem.ToString.Trim))
        End If
    End Sub

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
        DateTimePicker1.Visible = False
        TextBox4.Visible = False

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
        DateTimePicker1.Visible = True
        TextBox4.Visible = True

        Button4.Visible = True
    End Sub

    ' Muestra/oculta controles dependiendo si es coche nuevo o usado
    Private Sub mecanicoSeleccionado(mecanico As List(Of String))
        If mecanico IsNot Nothing Then
            TextBox1.Text = mecanico(0).Trim
            TextBox2.Text = mecanico(1).Trim
            TextBox3.Text = mecanico(2).Trim
            DateTimePicker1.Value = mecanico(3).Trim
            TextBox4.Text = mecanico(4).Trim
            unhide()
        End If
    End Sub

    ' Guardar
    Private Function cambios()
        If TextBox2.TextLength > 0 And TextBox3.TextLength > 0 And TextBox4.TextLength > 0 Then
            Dim regexSalario As New Regex("^\d{1,6}(?:\.\d{0,2})?$")
            Dim salario As String
            If TextBox4.Text.Trim.Contains(",") Then
                salario = TextBox4.Text.Trim.Replace(",", ".")
            Else
                salario = TextBox4.Text.Trim
            End If
            If regexSalario.IsMatch(salario) And TextBox4.TextLength > 2 Then
                ' Apertura de la conexion
                Form1.con.Open()
                ' Consultas, según si es usado o no
                Dim cmd As New SqlCommand("update mecanicos set nombre = '" + TextBox2.Text.Trim + "', apellidos = '" + TextBox3.Text.Trim + "', fechaContratacion = '" + DateTimePicker1.Value.ToString("yyyy-MM-dd") + "', salario = " + salario + "  where dni = '" + TextBox1.Text.Trim + "'", Form1.con)
                ' EJecucion de la consulta
                cmd.ExecuteNonQuery()
                Form1.con.Close()
                ' Actualizar listado de clientes, para tenerla siempre actualizada
                obtenerMecanicos()
            Else
                MsgBox("El salario debe tener al menos 3 dígitos y si es decimal separado con ." + vbLf + "Ejemplo: 1250.14")
            End If
        Else
            MsgBox("Rellene todos los datos")
        End If
    End Function

    ' Boton guardar
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        cambios()
    End Sub

    ' Boton y accion Eliminar
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ListBox1.SelectedItem IsNot Nothing Then
            ' Apertura de la conexion
            Form1.con.Open()
            ' Consultas
            Dim cmd As New SqlCommand("delete from mecanicos where dni = '" + ListBox1.SelectedItem.ToString.Trim + "'", Form1.con)
            ' EJecucion de la consulta
            cmd.ExecuteNonQuery()
            Form1.con.Close()
            ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
            hide()
        Else
            MsgBox("No se elegió ningún mecánico.")
        End If
    End Sub
End Class