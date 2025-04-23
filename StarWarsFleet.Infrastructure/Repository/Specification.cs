using System.Linq.Expressions;

namespace StarWarsFleet.Infrastructure.Repository;

public class Specification<T>
{
    public Expression<Func<T, bool>>? Criteria { get; }
    public List<Expression<Func<T, object>>> Includes { get; } = [];
    public Expression<Func<T, object>>? OrderBy { get; private set; }
    public Expression<Func<T, object>>? OrderByDescending { get; private set; }
    public List<string> IncludeString { get; private set; } = [];
    public int Take { get; private set; }
    public int Skip { get; private set; }
    public bool IsPagingEnabled { get; private set; }
    public bool IsNoTracking { get; private set; } = false;

    public Specification(){}

    public Specification(Expression<Func<T, bool>> criteria)
    {
        Criteria = criteria;
    }

    public Specification<T> AddInclude(Expression<Func<T, object>> includeExpression)
    {
        Includes.Add(includeExpression);
        return this;
    }

    public Specification<T> AddIncludeString(string includeString)
    {
        IncludeString.Add(includeString);
        return this;
    }

    public Specification<T> ApplyPaging(int skip, int take)
    {
        Skip = skip;
        Take = take;
        IsPagingEnabled = true;
        return this;
    }

    public Specification<T> ClearPaging()
    {
        Skip = 0;
        Take = 0;
        IsPagingEnabled = false;
        return this;
    }

    public Specification<T> ApplyOrderBy(Expression<Func<T, object>> orderByExpression)
    {
        OrderBy = orderByExpression;
        return this;
    }

    public Specification<T> ApplyOrderByDescending(Expression<Func<T, object>> orderByDescExpression)
    {
        OrderByDescending = orderByDescExpression;
        return this;
    }

    public Specification<T> ApplyNoTracking()
    {
        IsNoTracking = true;
        return this;
    }


}