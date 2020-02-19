Imports System.Drawing.Graphics
Imports Devart.Data.MySql
Public Class Seats
    Dim constr = "Host=localhost;port=3306;user=root;password=tiger;database=movie"
    Dim COMMAND As MySqlCommand
    Dim READER As MySqlDataReader
    Dim da As New MySqlDataAdapter
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Button4.Visible = False
        PrintForm1.Print()
        Me.Close()
    End Sub

    Private Sub Seats_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Label5.Text = Ticket.TextBox16.Text
        Label6.Text = Ticket.TextBox2.Text
        Label7.Text = Ticket.ComboBox1.Text
        Label8.Text = Ticket.TextBox4.Text
        Label9.Text = Ticket.TextBox5.Text
        Dim MysqlConn As New MySqlConnection(constr)
        Dim dt As DataTable
        Try
            MysqlConn.Open()
            COMMAND = New MySqlCommand
            With COMMAND
                .Connection = MysqlConn
                .CommandText = "SELECT photo FROM movie WHERE movie_name='" & Label5.Text & "'"
            End With
            da = New MySqlDataAdapter
            dt = New DataTable
            Dim arrImage() As Byte

            da.SelectCommand = COMMAND
            da.Fill(dt)

            arrImage = dt.Rows(0).Item(0)
            Dim mstream As New System.IO.MemoryStream(arrImage)
            PictureBox1.Image = Image.FromStream(mstream)

        Catch ex As Exception
            MsgBox(ex.Message)
        Finally
            da.Dispose()
            MysqlConn.Close()
        End Try
    End Sub
End Class