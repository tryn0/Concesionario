Imports System.Text.RegularExpressions
Imports System.Data.SqlClient

Public Class addVenta
    ' Boton salir
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        Me.Close()
    End Sub

    ' Introduce la venta en la BD, en fichaVentas
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        PictureBox1.Visible = True
        Dim regexMatricula As New Regex("^[0-9]{4,4}(?!.*(LL|CH))[BCDFGHJKLMNPRSTVWXYZ]{3}$")
        Dim regexDni As New Regex("^[0-9]{8,8}[A-Z]$")
        If regexMatricula.IsMatch(TextBox1.Text.ToUpper) Then
            If regexDni.IsMatch(TextBox2.Text.ToUpper) Then
                Dim insertado As Boolean = False
                Dim matricula As String = TextBox1.Text.ToUpper.Trim
                Dim existe As Boolean = comprobarExistePropietario(matricula)
                If existe = True Then
                    PictureBox1.Visible = False
                    MsgBox("Ya existe un dueño del coche introducido")
                Else
                    ' Apertura de la conexion
                    Form1.con.Open()
                    ' Consultas
                    Dim cmd As New SqlCommand("insert into fichaVentas (matricula, dniCliente) values('" + TextBox1.Text.ToUpper.Trim + "', '" + TextBox2.Text.ToUpper.Trim + "')", Form1.con)
                    ' EJecucion de la consulta
                    Try
                        cmd.ExecuteNonQuery()
                        insertado = True
                    Catch sqle As SqlException
                        insertado = False
                        PictureBox1.Visible = False
                        MsgBox("Debe introducir un DNI y mátricula existentes en la BD")
                    Finally
                        Form1.con.Close()
                    End Try

                    If insertado = True Then
                        PictureBox1.Visible = False
                        Ventas.obtenerCoches()
                        Me.Close()
                    End If
                End If
            Else
                PictureBox1.Visible = False
                MsgBox("Introduce un DNI válido")
            End If
        Else
            PictureBox1.Visible = False
            MsgBox("Introduce una matrícula válida")
        End If
    End Sub

    ' Comprobacion de matricula ya en fichaVentas, si es así significa que ya está vendido
    Private Function comprobarExistePropietario(matricula As String)
        Dim existeCoche As Boolean = False
        ' Apertura de la conexion
        Form1.con.Open()
        ' Consultas
        Dim cmd As New SqlCommand("select matricula from fichaVentas where matricula = '" + matricula + "'", Form1.con)
        ' EJecucion de la consulta
        Using rdr As SqlDataReader = cmd.ExecuteReader()
            ' Mientras la consulta devuelva datos
            While rdr.Read()
                ' Obtencion de datos y guardado en memoria
                rdr.GetString(0)
                existeCoche = True
            End While
            rdr.Close()
        End Using
        Form1.con.Close()

        ' Comprobacion del resultado de busqueda de coche en fichaVentas
        If existeCoche = True Then
            Return True
        Else
            Return False
        End If

    End Function
End Class