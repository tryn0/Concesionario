Imports System.Data.SqlClient
Imports System.Text.RegularExpressions

Public Class addMecanico
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        Me.Close()
    End Sub

    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        If TextBox1.TextLength > 0 And TextBox2.TextLength > 0 And TextBox3.TextLength > 0 And TextBox5.TextLength > 0 Then
            Dim regexDni As New Regex("^[0-9]{8,8}[A-Za-z]$")
            Dim regexSalario As New Regex("^\d{1,6}(?:\.\d{0,2})?$")
            ' Comprueba si el DNI y el salario son validos
            If regexDni.IsMatch(TextBox1.Text) Then
                Dim salario As String
                If TextBox5.Text.Trim.Contains(",") Then
                    salario = TextBox5.Text.Trim.Replace(",", ".")
                Else
                    salario = TextBox5.Text.Trim
                End If
                If regexSalario.IsMatch(salario) And TextBox5.TextLength > 2 Then
                    If Form4.mecanicos.Keys.Contains(TextBox1.Text.Trim()) Then
                        MsgBox("Ya existe un cliente con ese DNI")
                    Else
                        ' Apertura de la conexion
                        Form1.con.Open()
                        ' Consultas, según si es usado o no
                        Dim cmd As New SqlCommand("INSERT INTO mecanicos (dni, nombre, apellidos, fechaContratacion, salario) VALUES('" + TextBox1.Text.Trim.ToUpper + "', '" + TextBox2.Text.Trim + "', '" + TextBox3.Text.Trim + "', '" + DateTimePicker1.Value.ToString("yyyy-MM-dd") + "', " + salario + ")", Form1.con)
                        ' EJecucion de la consulta
                        cmd.ExecuteNonQuery()
                        Form1.con.Close()
                        Form4.obtenerMecanicos()
                        Me.Close()
                    End If
                Else
                    MsgBox("El salario debe tener al menos 3 dígitos y si es decimal separado con ." + vbLf + "Ejemplo: 1250.14")
                End If
            Else
                MsgBox("Compruebe el DNI del mecánico" + vbLf + "Debe tener este patrón 7 dígitos y 1 letra")
            End If
        Else
            MsgBox("Rellene el formulario completo para agregar un cliente")
        End If
    End Sub
End Class