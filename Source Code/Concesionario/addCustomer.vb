Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Public Class addCustomer
    ' Boton salir
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    ' Boton guardar, guarda cliente nuevo en BD
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.Text IsNot "" And TextBox2.Text IsNot "" And TextBox3.Text IsNot "" And TextBox4.Text IsNot "" And TextBox5.Text IsNot "" Then
            Dim regexDni As New Regex("^[0-9]{8,8}[A-Za-z]$")
            Dim regexTelefono As New Regex("^[6789]\d{8}$")
            ' Comprueba si el DNI es valido
            If regexDni.IsMatch(TextBox1.Text) Then
                If regexTelefono.IsMatch(TextBox5.Text) Then
                    If Form3.clientes.Keys.Contains(TextBox1.Text.Trim()) Then
                        MsgBox("Ya existe un cliente con ese DNI")
                    Else
                        ' Apertura de la conexion
                        Form1.con.Open()
                        ' Consultas, según si es usado o no
                        Dim cmd As New SqlCommand("INSERT INTO cliente (dni, nombre, apellidos, direccion, telefono) VALUES('" + (TextBox1.Text.Trim.Substring(0, 8) + TextBox1.Text.Trim.Substring(8).ToUpper) + "', '" + TextBox2.Text.Trim + "', '" + TextBox3.Text.Trim + "', '" + TextBox4.Text.Trim + "', " + TextBox5.Text.Trim + ")", Form1.con)
                        Debug.Print(cmd.CommandText)
                        ' EJecucion de la consulta
                        cmd.ExecuteNonQuery()
                        Form1.con.Close()

                        Form3.obtenerClientes()
                        Me.Close()
                    End If
                Else
                    MsgBox("Introduzca un teléfono válido")
                End If
            Else
                MsgBox("Compruebe el DNI del cliente" + vbLf + "Debe tener este patrón 7 dígitos y 1 letra")
            End If
        Else
            MsgBox("Rellene el formulario completo para agregar un cliente")
        End If
    End Sub
End Class