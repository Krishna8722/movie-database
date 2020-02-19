Imports Devart.Data.MySql
Public Class shows
    Dim constr = "Host=localhost;port=3306;user=root;password=tiger;database=movie"
    Dim COMMAND As MySqlCommand
    Dim READER As MySqlDataReader
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
        Admin_Home.Show()
    End Sub


    Private Sub Loadshow()
        Dim MysqlConn As New MySqlConnection(constr)
        Try
            Dim table As New DataTable()
            Dim adapter As New MySqlDataAdapter("SELECT * FROM shows", MysqlConn)
            adapter.Fill(table)
            DataGridView3.DataSource = table
            MysqlConn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try

    End Sub
    Private Sub movie()
        Dim MysqlConn As New MySqlConnection(constr)
        Dim adapter As New MySqlDataAdapter()
        Dim table As New DataTable()
        Dim ds As New DataSet()
        Dim Sql As String = "SELECT movie_name FROM movie"
        Try
            MysqlConn.Open()
            COMMAND = New MySqlCommand(Sql, MysqlConn)
            adapter.SelectCommand = COMMAND
            adapter.Fill(ds)
            adapter.Dispose()
            COMMAND.Dispose()
            ComboBox2.DataSource = ds.Tables(0)
            ComboBox2.ValueMember = "movie_name"
            ComboBox2.DisplayMember = "movie_name"
            MysqlConn.Close()
        Catch ex As Exception
            MessageBox.Show("Can not open connection ! ")
        End Try
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If (TextBox1.Text <> "" And TextBox16.Text <> "" And TextBox2.Text <> "" And TextBox3.Text <> "") Then
            Dim MysqlConn As New MySqlConnection(constr)
            Try
                MysqlConn.Open()
                Dim Query As String
                Query = "SELECT * FROM shows WHERE show_id ='" & TextBox1.Text & "'"
                COMMAND = New MySqlCommand(Query, MysqlConn)
                READER = COMMAND.ExecuteReader
                Dim count As Integer
                count = 0
                While READER.Read
                    count = count + 1
                End While
                If (count = 1) Then

                    Query = "DELETE FROM shows WHERE show_id ='" & TextBox1.Text & "'"
                    COMMAND = New MySqlCommand(Query, MysqlConn)
                    READER = COMMAND.ExecuteReader
                    MessageBox.Show("Show Deleted sucessfully")
                    MysqlConn.Close()
                    Loadshow()
                    TextBox1.Text = ""
                    TextBox16.Text = ""
                    TextBox2.Text = ""
                    TextBox3.Text = ""
                    ComboBox2.SelectedIndex = -1
                Else
                    MessageBox.Show("Record Not Found")
                End If
            Catch ex As MySqlException
                MessageBox.Show("Something Went Wrong Please Check the Details And Check Again Later!")
                TextBox1.Text = ""
                TextBox16.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                ComboBox2.SelectedIndex = -1
            Finally
                MysqlConn.Dispose()
            End Try
        Else
            MessageBox.Show("Please Enter All the Required Details")
            TextBox1.Text = ""
            TextBox16.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            ComboBox2.SelectedIndex = -1
        End If
    End Sub

    Private Sub shows_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Loadshow()
        movie()
        ComboBox2.SelectedIndex = -1

    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If (TextBox1.Text <> "" And TextBox16.Text <> "" And TextBox2.Text <> "" And TextBox3.Text <> "") Then
            Dim MysqlConn As New MySqlConnection(constr)
            Try
                MysqlConn.Open()
                Dim Query As String
                Query = "SELECT * FROM shows WHERE show_id ='" & TextBox1.Text & "'"
                COMMAND = New MySqlCommand(Query, MysqlConn)
                READER = COMMAND.ExecuteReader
                Dim count As Integer
                count = 0
                While READER.Read
                    count = count + 1
                End While
                If (count = 1) Then

                    Query = "Update shows SET show_name='" & TextBox16.Text & "', date  ='" & DateTimePicker1.Text & "', time  ='" & TextBox2.Text & "', movie  ='" & ComboBox2.Text & "', price  ='" & TextBox3.Text & "' WHERE show_id ='" & TextBox1.Text & "'"
                    COMMAND = New MySqlCommand(Query, MysqlConn)
                    READER = COMMAND.ExecuteReader
                    MessageBox.Show("Show Updated sucessfully")
                    MysqlConn.Close()
                    Loadshow()
                    TextBox1.Text = ""
                    TextBox16.Text = ""
                    TextBox2.Text = ""
                    TextBox3.Text = ""
                    ComboBox2.SelectedIndex = -1
                Else
                    MessageBox.Show("Record Not Found")
                End If
            Catch ex As MySqlException
                MessageBox.Show("Something Went Wrong Please Check the Details And Check Again Later!")
                TextBox1.Text = ""
                TextBox16.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                ComboBox2.SelectedIndex = -1
            Finally
                MysqlConn.Dispose()
            End Try
        Else
            MessageBox.Show("Please Enter All the Required Details")
            TextBox1.Text = ""
            TextBox16.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            ComboBox2.SelectedIndex = -1
        End If
    End Sub

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If (TextBox1.Text <> "" And TextBox16.Text <> "" And TextBox2.Text <> "" And TextBox3.Text <> "") Then
            Dim MysqlConn As New MySqlConnection(constr)
            Try
                MysqlConn.Open()
                Dim Query As String
                Query = "INSERT INTO shows VALUES ('" & TextBox1.Text & "','" & TextBox16.Text & "','" & DateTimePicker1.Text & "','" & TextBox2.Text & "','" & ComboBox2.Text & "','" & TextBox3.Text & "')"
                COMMAND = New MySqlCommand(Query, MysqlConn)
                READER = COMMAND.ExecuteReader
                MessageBox.Show("Show Inserted sucessfully")
                MysqlConn.Close()
                Loadshow()
                TextBox1.Text = ""
                TextBox16.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                ComboBox2.SelectedIndex = -1
            Catch ex As MySqlException
                MessageBox.Show("Something Went Wrong Please Check the Details And Check Again Later!")
                TextBox1.Text = ""
                TextBox16.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                ComboBox2.SelectedIndex = -1
            Finally
                MysqlConn.Dispose()
            End Try
        Else
            MessageBox.Show("Please Enter All the Required Details")
            TextBox1.Text = ""
            TextBox16.Text = ""
            TextBox2.Text = ""
            TextBox3.Text = ""
            ComboBox2.SelectedIndex = -1
        End If
    End Sub

    Private Sub DataGridView3_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView3.CellContentClick, DataGridView3.CellClick
        Dim index As Integer
        index = e.RowIndex
        Dim selectedRow As DataGridViewRow
        selectedRow = DataGridView3.Rows(index)

        TextBox1.Text = selectedRow.Cells(0).Value.ToString()
        TextBox16.Text = selectedRow.Cells(1).Value.ToString()
        DateTimePicker1.Text = selectedRow.Cells(2).Value.ToString()
        ComboBox2.Text = selectedRow.Cells(3).Value.ToString()
        TextBox2.Text = selectedRow.Cells(4).Value.ToString()
        TextBox3.Text = selectedRow.Cells(5).Value.ToString()

    End Sub
End Class