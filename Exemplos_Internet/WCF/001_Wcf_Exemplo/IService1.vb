<ServiceContract()> _
Public Interface IService1

    <OperationContract()> _
    Function ValidarEmail(ByVal email As String) As Boolean

End Interface

