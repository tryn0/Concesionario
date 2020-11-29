Imports System.Data.SqlClient

Public Class Form1
    Public con As New SqlConnection("data source=HP-OMEN-GUILLER;initial catalog=concesionario;integrated security=true;persist security info=True;")

    ' Salir
    Private Sub Button4_Click(sender As Object, e As EventArgs) Handles Button4.Click
        Me.Close()
    End Sub

    ' Coches
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click
        PictureBox1.Visible = True
        Form2.ShowDialog()
    End Sub

    'Clientes
    Private Sub Button2_Click(sender As Object, e As EventArgs) Handles Button2.Click
        PictureBox1.Visible = True
        Form3.ShowDialog()
    End Sub

    ' Mecanicos
    Private Sub Button3_Click(sender As Object, e As EventArgs) Handles Button3.Click
        PictureBox1.Visible = True
        Form4.ShowDialog()
    End Sub

    ' Muestra/Añade todas las transacciones de ventas de coches
    Private Sub Button5_Click(sender As Object, e As EventArgs) Handles Button5.Click
        PictureBox1.Visible = True
        Ventas.ShowDialog()
    End Sub

    ' Muestra/Añade todas las reparaciones de coches
    Private Sub Button6_Click(sender As Object, e As EventArgs) Handles Button6.Click
        PictureBox1.Visible = True
        Reparaciones.ShowDialog()
    End Sub

    ' Mostrar los coches que tiene cada cliente, sease indice de ventas
    Private Sub Button7_Click(sender As Object, e As EventArgs) Handles Button7.Click
        PictureBox1.Visible = True
        Coches.ShowDialog()
    End Sub
End Class
