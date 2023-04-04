namespace Mysennger.Domain;

public static class Constants
{
    public static class Subriddot 
    {
        public const int NameMaxLength = 32;
        public const int DescriptionMaxLength = 256;
    }

    public static class Comment
    {
        public const int ContentMaxLength = 128;
    }

    public static class Post
    {
        public const int ContentMaxLength = 128;
    }

    public static class User
    {
        public const int NameMaxLength = 32;
        public const int PasswordMinLength = 8;
    }
}