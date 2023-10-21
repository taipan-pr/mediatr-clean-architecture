using MediatR;

namespace MediatrCleanArchitecture.Application.Queries.GetAllEmployee;

public record GetAllEmployeeRequest : IRequest<IEnumerable<GetAllEmployeeResponse>> { }
