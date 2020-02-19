Imports Devart.Data.MySql
Public Class Category
    Dim constr = "Host=localhost;port=3306;user=root;password=tiger;database=movie"
    Dim COMMAND As MySqlCommand
    Dim READER As MySqlDataReader
    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
        Admin_Home.Show()
    End Sub


    Private Sub Loadcate()
        Dim MysqlConn As New MySqlConnection(constr)
        Try
            Dim table As New DataTable()
            Dim adapter As New MySqlDataAdapter("SELECT * FROM category", MysqlConn)
            adapter.Fill(table)
            DataGridView3.DataSource = table
            MysqlConn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try

    End Sub
    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        If (TextBox1.Text <> "" And TextBox16.Text <> "" And TextBox2.Text <> "") Then

            Dim MysqlConn As New MySqlConnection(constr)
            Try
                MysqlConn.Open()
                Dim Query As String
                Dim n As Integer
                n = Convert.ToInt16(TextBox2.Text)
                Query = "insert into category values ('" & TextBox1.Text & "','" & TextBox16.Text & "','" & n & "')"
                COMMAND = New MySqlCommand(Query, MysqlConn)
                READER = COMMAND.ExecuteReader
                MessageBox.Show("Category Saved sucessfully")
                MysqlConn.Close()
                Loadcate()
                TextBox1.Text = ""
                TextBox16.Text = ""
                TextBox2.Text = ""
            Catch ex As MySqlException
                MessageBox.Show("Something Went Wrong Please Check the Details And Check Again Later!")
                TextBox1.Text = ""
                TextBox16.Text = ""
                TextBox2.Text = ""
            Finally
                MysqlConn.Dispose()
            End Try
        Else
            MessageBox.Show("Please Enter All the Required Details")
            TextBox1.Text = ""
            TextBox16.Text = ""
            TextBox2.Text = ""
        End If
    End Sub

    Private Sub Button2_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button2.Click
        If (TextBox1.Text <> "" And TextBox16.Text <> "" And TextBox2.Text <> "") Then

            Dim MysqlConn As New MySqlConnection(constr)
            Try
                MysqlConn.Open()
                Dim Query As String
                Query = "SELECT * FROM category WHERE category_id ='" & TextBox1.Text & "'"
                COMMAND = New MySqlCommand(Query, MysqlConn)
                READER = COMMAND.ExecuteReader
                Dim count As Integer
                count = 0
                While READER.Read
                    count = count + 1
                End While
                If (count = 1) Then

                    Query = "Update category SET category_name='" & TextBox16.Text & "',no_of_seats ='" & TextBox2.Text & "' WHERE category_id ='" & TextBox1.Text & "'"
                    COMMAND = New MySqlCommand(Query, MysqlConn)
                    READER = COMMAND.ExecuteReader
                    MessageBox.Show("Category Updated sucessfully")
                    MysqlConn.Close()
                    Loadcate()
                    TextBox1.Text = ""
                    TextBox16.Text = ""
                    TextBox2.Text = ""
                Else
                    MessageBox.Show("Record Not Found")
                End If
            Catch ex As MySqlException
                MessageBox.Show("Something Went Wrong Please Check the Details And Check Again Later!")
                TextBox1.Text = ""
                TextBox16.Text = ""
                TextBox2.Text = ""
            Finally
                MysqlConn.Dispose()
            End Try
        Else
            MessageBox.Show("Please Enter All the Required Details")
            TextBox1.Text = ""
            TextBox16.Text = ""
            TextBox2.Text = ""
        End If
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If (TextBox1.Text <> "" And TextBox16.Text <> "" And TextBox2.Text <> "") Then
            Dim MysqlConn As New MySqlConnection(constr)
            Try
                MysqlConn.Open()
                Dim Query As String
                Query = "SELECT * FROM category WHERE category_id ='" & TextBox1.Text & "'"
                COMMAND = New MySqlCommand(Query, MysqlConn)
                READER = COMMAND.ExecuteReader
                Dim count As Integer
                count = 0
                While READER.Read
                    count = count + 1
                End While
                If (count = 1) Then

                    Query = "DELETE FROM category WHERE category_id ='" & TextBox1.Text & "'"
                    COMMAND = New MySqlCommand(Query, MysqlConn)
                    READER = COMMAND.ExecuteReader
                    MessageBox.Show("Category Deleted sucessfully")
                    MysqlConn.Close()
                    Loadcate()
                    TextBox1.Text = ""
                    TextBox16.Text = ""
                    TextBox2.Text = ""
                Else
                    MessageBox.Show("Record Not Found")
                End If
            Catch ex As MySqlException
                MessageBox.Show("Something Went Wrong Please Check the Details And Check Again Later!")
                TextBox1.Text = ""
                TextBox16.Text = ""
                TextBox2.Text = ""
            Finally
                MysqlConn.Dispose()
            End Try
        Else
            MessageBox.Show("Please Enter All the Required Details")
            TextBox1.Text = ""
            TextBox16.Text = ""
            TextBox2.Text = ""
        End If
    End Sub

    Private Sub Category_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Loadcate()
    End Sub

    Private Sub DataGridView3_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView3.CellContentClick, DataGridView3.CellClick
        Dim index As Integer
        index = e.RowIndex
        Dim selectedRow As DataGridViewRow
        selectedRow = DataGridView3.Rows(index)

        TextBox1.Text = selectedRow.Cells(0).Value.ToString()
        TextBox16.Text = selectedRow.Cells(1).Value.ToString()
        TextBox2.Text = selectedRow.Cells(2).Value.ToString()
    End Sub
End Class