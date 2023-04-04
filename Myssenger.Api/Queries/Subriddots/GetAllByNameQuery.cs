using MediatR;
using Myssenger.Api.Dtos;

namespace Myssenger.Api.Queries.Subriddots;

public sealed record GetAllByNameQuery(string Name) : IRequest<IEnumerable<GetSubriddotDto>>;