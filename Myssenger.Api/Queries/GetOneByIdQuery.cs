using MediatR;
using Myssenger.Api.Dtos;
using OneOf.Monads;

namespace Myssenger.Api.Queries;

public sealed record GetOneByIdQuery(Guid SubriddotId) : IRequest<Result<KeyNotFoundException, GetSubriddotDto>>;