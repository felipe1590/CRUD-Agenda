Imports System.Collections.ObjectModel
Imports System.Data.SqlClient
Imports MySql.Data.MySqlClient

Public Class Form1
    Private Sub Form1_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        desabilitaCampos()
        listarContatos()

        btnIncluir.Enabled = False
        btnExcluir.Enabled = False
        btnAlterar.Enabled = False
    End Sub

    Sub habilitaCampos()
        txtNome.Enabled = True
        txtEndereco.Enabled = True
        txtTelefone.Enabled = True
        txtEmail.Enabled = True
    End Sub

    Sub desabilitaCampos()
        txtNome.Enabled = False
        txtEndereco.Enabled = False
        txtTelefone.Enabled = False
        txtEmail.Enabled = False
    End Sub

    Sub limpaCampos()
        txtId.Text = ""
        txtNome.Text = ""
        txtEndereco.Text = ""
        txtTelefone.Text = ""
        txtEmail.Text = ""
    End Sub

    Public Sub listarContatos()

        Dim dt As New DataTable
        Dim da As New MySqlDataAdapter

        Try
            Abrir()
            da = New MySqlDataAdapter("SELECT * FROM contatos", con)
            da.Fill(dt)
            dgContatos.DataSource = dt

            formataDG()
        Catch ex As Exception
            MessageBox.Show("Erro ao listar" + ex.Message)
            Fechar()
        End Try

    End Sub

    Sub formataDG()
        dgContatos.Columns(0).Visible = False
        dgContatos.Columns(1).HeaderText = "Nome"
        dgContatos.Columns(2).HeaderText = "Endereço"
        dgContatos.Columns(3).HeaderText = "Telefone"
        dgContatos.Columns(4).HeaderText = "Email"
    End Sub

    Private Sub btnNovo_Click(sender As Object, e As EventArgs) Handles btnNovo.Click
        habilitaCampos()
        limpaCampos()

        btnIncluir.Enabled = True
        btnAlterar.Enabled = False
        btnExcluir.Enabled = False
    End Sub

    Private Sub btnIncluir_Click(sender As Object, e As EventArgs) Handles btnIncluir.Click

        If txtNome.Text = "" Then
            MsgBox("Preenxa o Nome do Contato")
            txtNome.Focus()
        ElseIf txtEndereco.Text = "" Then
            MsgBox("Preenxa o Endereco do Contato")
            txtEndereco.Focus()
        ElseIf Not txtTelefone.MaskCompleted Then
            MsgBox("Preenxa o Telefone do Contato")
            txtTelefone.Focus()
        ElseIf txtEmail.Text = "" Then
            MsgBox("Preenxa o Email do Contato")
        Else
            Dim cmd As New MySqlCommand
            Dim sql As String
            Try
                sql = "INSERT INTO contatos(nome, endereco, telefone, email) VALUES ('" & txtNome.Text & "', '" & txtEndereco.Text & "', '" & txtTelefone.Text & "', '" & txtEmail.Text & "')"
                cmd = New MySqlCommand(sql, con)
                cmd.ExecuteNonQuery()
                MessageBox.Show("Contato Salvo com Sucesso!")

                desabilitaCampos()
                limpaCampos()
                listarContatos()

                btnIncluir.Enabled = False

            Catch ex As Exception
                MessageBox.Show("Erro ao Salvar" + ex.Message)
                Fechar()
            End Try

        End If
    End Sub

    Private Sub btnAlterar_Click(sender As Object, e As EventArgs) Handles btnAlterar.Click
        If txtId.Text <> "" Then
            Dim cmd As New MySqlCommand
            Dim sql As String
            Try
                sql = "UPDATE contatos SET nome ='" & txtNome.Text & "',`endereco`='" & txtEndereco.Text & "',`telefone`='" & txtTelefone.Text & "',`email`='" & txtEmail.Text & "' WHERE id = '" & txtId.Text & "'"
                cmd = New MySqlCommand(sql, con)
                cmd.ExecuteNonQuery()
                MessageBox.Show("Contato Alterado com Sucesso!")

                desabilitaCampos()
                limpaCampos()
                listarContatos()

                btnAlterar.Enabled = False
                btnExcluir.Enabled = False

            Catch ex As Exception
                MessageBox.Show("Erro ao Salvar" + ex.Message)
                Fechar()
            End Try
        End If
    End Sub

    Private Sub btnExcluir_Click(sender As Object, e As EventArgs) Handles btnExcluir.Click
        If txtId.Text <> "" Then
            Dim cmd As New MySqlCommand
            Dim sql As String
            Try
                sql = "DELETE FROM contatos WHERE id = '" & txtId.Text & "'"
                cmd = New MySqlCommand(sql, con)
                cmd.ExecuteNonQuery()
                MessageBox.Show("Contato Excluído com Sucesso!")

                desabilitaCampos()
                limpaCampos()
                listarContatos()

                btnAlterar.Enabled = False
                btnExcluir.Enabled = False

            Catch ex As Exception
                MessageBox.Show("Erro ao Salvar" + ex.Message)
                Fechar()
            End Try
        End If
    End Sub

    Private Sub dgContatos_CellDoubleClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgContatos.CellDoubleClick
        txtId.Text = dgContatos.CurrentRow.Cells(0).Value
        txtNome.Text = dgContatos.CurrentRow.Cells(1).Value
        txtEndereco.Text = dgContatos.CurrentRow.Cells(2).Value
        txtTelefone.Text = dgContatos.CurrentRow.Cells(3).Value
        txtEmail.Text = dgContatos.CurrentRow.Cells(4).Value

        habilitaCampos()
        btnIncluir.Enabled = False
        btnAlterar.Enabled = True
        btnExcluir.Enabled = True
    End Sub
End Class
