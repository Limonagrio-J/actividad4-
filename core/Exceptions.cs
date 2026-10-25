namespace ApiInicial.Core;

/// <summary>El recurso solicitado no existe (HTTP 404).</summary>
public class NotFoundException(string message) : Exception(message);

/// <summary>La petición es inválida por reglas de negocio (HTTP 400).</summary>
public class BadRequestException(string message) : Exception(message);

/// <summary>La operación entra en conflicto con el estado actual (HTTP 409).</summary>
public class ConflictException(string message) : Exception(message);
