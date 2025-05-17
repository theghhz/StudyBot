namespace StudyBot.Domain.Erros
{
    public class Either
    {
        public static Either<T, V> OnLeft<T, V>(T left)
        {
            return left;
        }

        public static Either<T, V> OnRight<T, V>(V right)
        {
            return right;
        }
    }

    public class Either<T, V>
    {
        protected bool isLeft;

        protected Either(T left)
        {
            Left = left;
            Right = default;
            isLeft = true;
        }

        protected Either(V right)
        {
            Left = default;
            Right = right;
            isLeft = false;
        }

        public T? Left { get; protected set; }

        public V? Right { get; protected set; }

        public bool IsLeft => isLeft;

        public bool IsRight => !isLeft;

        public static implicit operator Either<T, V>(T left)
        {
            return new Either<T, V>(left);
        }

        public static implicit operator Either<T, V>(V right)
        {
            return new Either<T, V>(right);
        }
    }
}