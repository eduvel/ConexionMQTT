Imports System.IO.Ports
Imports System.Net.Sockets
Public Class Form1
    Dim clientSocket As New System.Net.Sockets.TcpClient()
    Private Sub Button1_Click(sender As Object, e As EventArgs) Handles Button1.Click

        Dim serverStream As NetworkStream
        Dim dato As String = "103d00064d514973647003c2003c00176d6f73717075627c393838302d4e54424b2d47332d454100086e6a75667a746f6f000c716e6836574e527352446c64"
    'Dim DATO2 As String = "301300092f53656374696f6e31313533614a4a5467E000"
    Dim DATO2 As String = "821e0001001962696f6469676573746f7243482f54656d706572617475726100"
    Dim Dato3 As String = "9003000100"
    Dim Dato4 As String = "e000"
    Try
    Try


    Dim port As Integer = 16476
                clientSocket.Connect("tailor.cloudmqtt.com", port)
                Dim data(dato.Length() \ 2) As Byte
    For j = 0 To dato.Length() - 1 Step 2
                    data(j \ 2) = (HexToDec(dato.Substring(j, 2)))

                Next

                serverStream = clientSocket.GetStream()


                serverStream.Write(data, 0, data.Length)

                Debug.Print("dato enviado")




                Dim data2(DATO2.Length() \ 2) As Byte
    For j = 0 To DATO2.Length() - 1 Step 2
                    data2(j \ 2) = (HexToDec(DATO2.Substring(j, 2)))

                Next

                'serverStream = clientSocket.GetStream()
                'serverStream.Flush()

                serverStream.Write(data2, 0, data2.Length)

                'InputBox("ESPERAR")
                Dim datar(256) As Byte
                Console.WriteLine("recibir")

                Dim responseData As String = String.Empty


    Dim bytes As Int32
                Do
                    bytes = serverStream.Read(datar, 0, datar.Length)
                    Console.WriteLine(bytes)
                Loop While bytes < 75

                'serverStream = clientSocket.GetStream()
                responseData = System.Text.Encoding.ASCII.GetString(datar, 0, bytes)
                Console.WriteLine("Received: {0}", responseData)

                Dim data3(Dato3.Length() \ 2) As Byte
    For j = 0 To Dato3.Length() - 1 Step 2
                    data3(j \ 2) = (HexToDec(Dato3.Substring(j, 2)))

                Next

                serverStream.Write(data3, 0, data3.Length)
                Dim data4(Dato4.Length() \ 2) As Byte
    For j = 0 To Dato4.Length() - 1 Step 2
                    data4(j \ 2) = (HexToDec(Dato4.Substring(j, 2)))

                Next

                serverStream.Write(data4, 0, data4.Length)
                'Do While clientSocket.Avviailable() > 0
                'serverStream = clientSocket.GetStream()
                'bytes = serverStream.Read(datar, 0, datar.Length)
                'responseData = System.Text.Encoding.ASCII.GetString(datar, 0, bytes)
                'Console.WriteLine("Received: {-}", responseData)
                'Loop
                ' Console.WriteLine("Received: {1}", datar.Length)

                'bytes = serverStream.Read(datar, 0, datar.Length)
                'Do While bytes > 0

                'Loop
                'responseData = System.Text.Encoding.ASCII.GetString(datar, 0, bytes)
                'Console.WriteLine("Received: {1}", responseData)


                serverStream.Close()
                clientSocket.Close()

            Catch ex As ArgumentNullException

                Console.WriteLine("ArgumentNullException: {0}", ex)
            End Try
    Catch ex As SocketException

            Console.WriteLine("SocketException: {0}", ex)
        End Try
    End Sub
    Public Function HexToDec(ByVal HexStr As String) As Double
        Dim mult As Double
        Dim DecNum As Double
        Dim ch As String
        mult = 1
        DecNum = 0

        Dim i As Integer
        For i = Len(HexStr) To 1 Step -1
            ch = Mid(HexStr, i, 1)
            If (ch >= "0") And (ch <= "9") Then
                DecNum = DecNum + (Val(ch) * mult)
            Else
                If (ch >= "A") And (ch <= "F") Then
                    DecNum = DecNum + ((Asc(ch) - Asc("A") + 10) * mult)
                Else
                    If (ch >= "a") And (ch <= "f") Then
                        DecNum = DecNum + ((Asc(ch) - Asc("a") + 10) * mult)
                    Else
                        HexToDec = 0
                        Exit Function
                    End If
                End If
            End If
            mult = mult * 16
        Next i
        HexToDec = DecNum
    End Function


End Class
