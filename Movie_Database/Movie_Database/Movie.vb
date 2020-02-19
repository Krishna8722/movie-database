Imports Devart.Data.MySql
Public Class Movie
    Dim constr = "Host=localhost;port=3306;user=root;password=tiger;database=movie"
    Dim COMMAND As MySqlCommand
    Dim READER As MySqlDataReader
    Private Sub Movie_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Loadmovie()
    End Sub
    Private Sub Loadmovie()
        Dim MysqlConn As New MySqlConnection(constr)
        Try
            Dim table As New DataTable()
            Dim adapter As New MySqlDataAdapter("SELECT movie_id,movie_name,description FROM movie", MysqlConn)
            adapter.Fill(table)
            DataGridView3.DataSource = table
            MysqlConn.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message)
        Finally
            MysqlConn.Dispose()
        End Try

    End Sub

    Private Sub Button4_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button4.Click
        Me.Close()
        Admin_Home.Show()
    End Sub

    Private Sub Button3_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button3.Click
        If (TextBox1.Text <> "" And TextBox16.Text <> "" And TextBox2.Text <> "") Then
            Dim MysqlConn As New MySqlConnection(constr)
            Try
                MysqlConn.Open()
                Dim Query As String
                Query = "SELECT * FROM movie WHERE movie_id ='" & TextBox1.Text & "'"
                COMMAND = New MySqlCommand(Query, MysqlConn)
                READER = COMMAND.ExecuteReader
                Dim count As Integer
                count = 0
                While READER.Read
                    count = count + 1
                End While
                If (count = 1) Then

                    Query = "DELETE FROM movie WHERE movie_id ='" & TextBox1.Text & "'"
                    COMMAND = New MySqlCommand(Query, MysqlConn)
                    READER = COMMAND.ExecuteReader
                    MessageBox.Show("Movie Deleted sucessfully")
                    MysqlConn.Close()
                    Loadmovie()
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

    

    Private Sub Button1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button1.Click
        Try
            If (TextBox1.Text <> "" And TextBox16.Text <> "" And TextBox2.Text <> "") Then
                Dim con As MySqlConnection = New MySqlConnection("Host=localhost;port=3306;user=root;password=tiger;database=movie")
                Dim cmd As MySqlCommand
                Dim sql As String
                Dim result As Integer
                Dim caption As String
                Dim arrImage() As Byte
                Dim mstream As New System.IO.MemoryStream()

                caption = System.IO.Path.GetFileName(OpenFileDialog1.FileName)
                'SPECIFIES THE FILE FORMAT OF THE IMAGE
                PictureBox1.Image.Save(mstream, System.Drawing.Imaging.ImageFormat.Jpeg)

                'RETURNS THE ARRAY OF UNSIGNED BYTES FROM WHICH THIS STREAM WAS CREATED
                arrImage = mstream.GetBuffer()

                'GET THE SIZE OF THE STREAM IN BYTES
                Dim FileSize As UInt32
                FileSize = mstream.Length
                'CLOSES THE CURRENT STREAM AND RELEASE ANY RESOURCES ASSOCIATED WITH THE CURRENT STREAM
                mstream.Close()
                Try
                    con.Open()
                    sql = "INSERT INTO  movie VALUES (@movie_id,@movie_name,@description,@photo)"
                    cmd = New MySqlCommand
                    With cmd
                        .Connection = con
                        .CommandText = sql
                        .Parameters.AddWithValue("@movie_id", TextBox1.Text)
                        .Parameters.AddWithValue("@movie_name", TextBox16.Text)
                        .Parameters.AddWithValue("@description", TextBox2.Text)
                        .Parameters.AddWithValue("@photo", arrImage)
                        result = .ExecuteNonQuery()

                    End With
                    If result > 0 Then
                        Loadmovie()
                        MsgBox("Movie Record has been saved in the database")
                    Else
                        MsgBox("Error query", MsgBoxStyle.Exclamation)
                    End If
                Catch ex As Exception
                    MsgBox(ex.Message)
                Finally
                    con.Close()
                End Try
                PictureBox1.Image = Nothing
            Else
                MessageBox.Show("Please Enter All the Required Details")
                TextBox1.Text = ""
                TextBox16.Text = ""
                TextBox2.Text = ""
            End If
        Catch ex As Exception
            MessageBox.Show("something went wrong please try again")
        End Try
       
    End Sub

    Private Sub Button5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Button5.Click
        Try
            With OpenFileDialog1

                'CHECK THE SELECTED FILE IF IT EXIST OTHERWISE THE DIALOG BOX WILL DISPLAY A WARNING.
                .CheckFileExists = True

                'CHECK THE SELECTED PATH IF IT EXIST OTHERWISE THE DIALOG BOX WILL DISPLAY A WARNING.
                .CheckPathExists = True

                'GET AND SET THE DEFAULT EXTENSION
                .DefaultExt = "jpg"

                'RETURN THE FILE LINKED TO THE LNK FILE
                .DereferenceLinks = True

                'SET THE FILE NAME TO EMPTY 
                .FileName = ""

                'FILTERING THE FILES
                .Filter = "(*.jpg)|*.jpg|(*.png)|*.png|(*.jpg)|*.jpg|All files|*.*"
                'SET THIS FOR ONE FILE SELECTION ONLY.
                .Multiselect = False



                'SET THIS TO PUT THE CURRENT FOLDER BACK TO WHERE IT HAS STARTED.
                .RestoreDirectory = True

                'SET THE TITLE OF THE DIALOG BOX.
                .Title = "Select a file to open"

                'ACCEPT ONLY THE VALID WIN32 FILE NAMES.
                .ValidateNames = True

                If .ShowDialog = DialogResult.OK Then
                    Try
                        PictureBox1.Image = Image.FromFile(OpenFileDialog1.FileName)
                    Catch fileException As Exception
                        Throw fileException
                    End Try
                End If

            End With
        Catch ex As Exception
            MsgBox(ex.Message, MsgBoxStyle.Exclamation, Me.Text)
        End Try
    End Sub

    Private Sub DataGridView3_CellContentClick(ByVal sender As System.Object, ByVal e As System.Windows.Forms.DataGridViewCellEventArgs) Handles DataGridView3.CellContentClick, DataGridView3.CellClick
        Dim index As Integer
        index = e.RowIndex
        Dim selectedRow As DataGridViewRow
        selectedRow = DataGridView3.Rows(index)

        TextBox1.Text = selectedRow.Cells(0).Value.ToString()
        TextBox16.Text = selectedRow.Cells(1).Value.ToString()
        TextBox2.Text = selectedRow.Cells(2).Value.ToString()
        PictureBox1.Image = Nothing
    End Sub
End Class