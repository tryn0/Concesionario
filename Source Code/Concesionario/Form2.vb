Imports System.Data.SqlClient

Public Class Form2

    ' Diccionario con los datos de los coches
    Public coches As New Dictionary(Of String, List(Of String))

    ' Boton agregar
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        addCar.ShowDialog()
    End Sub

    ' Boton salir
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        coches.Clear()
        hide()
        Me.Close()
    End Sub

    ' Al cargar el formulario ejecuta obtenerCoches y ordena la lista, en este caso por la matricula
    Private Sub Form2_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        obtenerCoches()
        ListBox1.Sorted = True
    End Sub

    ' Obtener coches de la BD e introducirla en la listbox
    Public Sub obtenerCoches()
        coches.Clear()
        ListBox1.Items.Clear()
        ' Apertura de la conexion
        Form1.con.Open()
        ' Consultas
        Dim cmd As New SqlCommand("select * from coches", Form1.con)
        ' EJecucion de la consulta
        Using rdr As SqlDataReader = cmd.ExecuteReader()
            ' Mientras la consulta devuelva datos
            While rdr.Read()
                ' Obtencion de datos y guardado en memoria
                Dim info As New List(Of String) From {rdr.GetString(0), rdr.GetInt32(1).ToString(), rdr.GetString(2), rdr.GetString(3), rdr.GetString(4), rdr.GetInt32(5).ToString(), rdr.GetDateTime(6).ToString(), rdr.GetInt32(7).ToString()}
                coches.Add(rdr.GetString(0), info)
            End While
            rdr.Close()
        End Using
        Form1.con.Close()

        ' Iteracion sobre los trabajadores para agregarlos al listbox1
        For Each coche As KeyValuePair(Of String, List(Of String)) In coches
            ListBox1.Items.Add(coche.Value(0))
            If coche.Value(0) = coches.Last().Value(0) Then
                Form1.PictureBox1.Visible = False
            End If
        Next
    End Sub

    ' Al seleccionar elementos de la listbox obtiene datos del coche y es introducida en sus respectivos controles
    Private Sub ListBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ListBox1.SelectedIndexChanged
        cocheSeleccionado(getCoche())
    End Sub

    ' Devuelve la lista de datos del coche seleccionado en el listbox
    Private Function getCoche()
        If ListBox1.SelectedItem IsNot Nothing Then
            Dim matricula As String = ListBox1.SelectedItem
            For Each coche As KeyValuePair(Of String, List(Of String)) In coches
                If coche.Value(0).Trim() = matricula.Trim() Then
                    Return coche.Value
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
        ComboBox1.Visible = False
        TextBox3.Visible = False
        TextBox4.Visible = False
        TextBox5.Visible = False
        Label6.Visible = False
        Label7.Visible = False
        Label8.Visible = False
        TextBox6.Visible = False
        DateTimePicker1.Visible = False
        TextBox8.Visible = False
        Button4.Visible = False
    End Sub

    ' Mostrar controles
    Private Sub unhide(usado As Integer)
        Label1.Visible = True
        Label2.Visible = True
        Label3.Visible = True
        Label4.Visible = True
        Label5.Visible = True
        TextBox1.Visible = True
        ComboBox1.Visible = True
        TextBox3.Visible = True
        TextBox4.Visible = True
        TextBox5.Visible = True

        If usado = 1 Then
            Label6.Visible = True
            Label7.Visible = True
            Label8.Visible = False
            TextBox6.Visible = True
            DateTimePicker1.Visible = True
            TextBox8.Visible = False
        Else
            Label6.Visible = False
            Label7.Visible = False
            Label8.Visible = True
            TextBox6.Visible = False
            DateTimePicker1.Visible = False
            TextBox8.Visible = True
        End If
        Button4.Visible = True
    End Sub

    ' Muestra/oculta controles dependiendo si es coche nuevo o usado
    Private Sub cocheSeleccionado(coche As List(Of String))
        If coche IsNot Nothing Then
            TextBox1.Text = coche(0).Trim
            TextBox3.Text = coche(3).Trim
            TextBox4.Text = coche(2).Trim
            TextBox5.Text = coche(4).Trim
            If coche(1) = 1 Then
                TextBox6.Text = coche(5).Trim
                DateTimePicker1.Value = coche(6)
                unhide(1)
                ComboBox1.Text = "Sí"
            Else
                TextBox8.Text = coche(7).Trim
                unhide(0)
                ComboBox1.Text = "No"
            End If
        End If
    End Sub

    ' Guardar cambios del coche en BD
    Private Function cambios(matricula As String)
        For Each coche As KeyValuePair(Of String, List(Of String)) In coches
            If coche.Value(0).Trim() = matricula.Trim() Then
                ' Apertura de la conexion
                Form1.con.Open()
                ' Consultas, según si es usado o no
                Dim cmd As New SqlCommand
                If ComboBox1.SelectedItem = "Sí" Then
                    cmd = New SqlCommand("update coches set usado = 1, modelo = '" + TextBox3.Text.Trim() + "', marca = '" + TextBox4.Text.Trim() + "', color = '" + TextBox5.Text.Trim() + "', kilometros = " + TextBox6.Text.Trim() + ", anioMatriculacion = '" + DateTimePicker1.Value.ToString("yyyy-MM-dd") + "', numeroUnidades = 0 where matricula = '" + matricula.Trim() + "'", Form1.con)
                Else
                    Dim ahora As String = DateTime.Now.ToString("yyyy-MM-dd")
                    cmd = New SqlCommand("update coches set usado = 0, modelo = '" + TextBox3.Text.Trim() + "', marca = '" + TextBox4.Text.Trim() + "', color = '" + TextBox5.Text.Trim() + "', numeroUnidades = " + TextBox8.Text.Trim() + ", anioMatriculacion = '" + ahora + "', kilometros = 0 where matricula = '" + matricula.Trim() + "'", Form1.con)
                End If
                ' EJecucion de la consulta
                cmd.ExecuteNonQuery()
                Form1.con.Close()
            End If
        Next
        ' Actualizar listado de coches, para tenerla siempre actualizada
        obtenerCoches()
    End Function

    ' Boton guardar
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        cambios(getCoche(0))
    End Sub

    ' Muestra/oculta controles al elegir usado o no
    Private Sub ComboBox1_SelectedIndexChanged(sender As Object, e As EventArgs) Handles ComboBox1.SelectedIndexChanged
        If ComboBox1.SelectedItem = "Sí" Then
            Label6.Visible = True
            Label7.Visible = True
            Label8.Visible = False
            TextBox6.Visible = True
            DateTimePicker1.Visible = True
            TextBox8.Visible = False
        Else
            Label6.Visible = False
            Label7.Visible = False
            Label8.Visible = True
            TextBox6.Visible = False
            DateTimePicker1.Visible = False
            TextBox8.Visible = True
        End If
    End Sub

    ' Boton y accion Eliminar
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        If ListBox1.SelectedItem IsNot Nothing Then
            ' Apertura de la conexion
            Form1.con.Open()
            ' Consultas
            Dim cmd As New SqlCommand("delete from coches where matricula = '" + getCoche()(0) + "'", Form1.con)
            ' EJecucion de la consulta
            cmd.ExecuteNonQuery()
            Form1.con.Close()
            ListBox1.Items.RemoveAt(ListBox1.SelectedIndex)
            hide()
        Else
            MsgBox("No se elegió ningún coche.")
        End If
    End Sub
End Class