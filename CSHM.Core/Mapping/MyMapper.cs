using AutoMapper;
using CSHM.Domain;
using CSHM.Widget.Calendar;
using CSHM.Presentations.User;
using CSHM.Presentations.Role;
using CSHM.Presentation.Blog;
using CSHM.Presentation.Publish;
using CSHM.Presentation.Product;
using CSHM.Presentations.Media;
using CSHM.Presentation.Occupation;
using CSHM.Presentation.People;
using CSHM.Presentation.Lable;
using CSHM.Presentation.Language;
using CSHM.Presentation.Category;
using CSHM.Presentation.Size;
using CSHM.Presentation.Cover;
using CSHM.Domain.Models;
using CSHM.Presentation.Notification;
using CSHM.Presentations.Login;

namespace CSHM.Core.Mapping;

public class MyMapper : Profile
{
    public MyMapper()
    {
        //=============================================================== IDENTITY

        CreateMap<PolicyParameter, PolicyParameterViewModel>();
        
        CreateMap<Policy, PolicyViewModel>();
        
        CreateMap<UserPolicy, UserPolicyViewModel>()
            .ForMember(x => x.PolicyParameterID, opt => opt.MapFrom(origin => origin.Policy.PolicyParameterID))
            .ForMember(x => x.PolicyParameterTitle, opt => opt.MapFrom(origin => origin.Policy.PolicyParameter.Title))
            .ForMember(x => x.Key, opt => opt.MapFrom(origin => origin.Policy.Key))
            .ForMember(x => x.Value, opt => opt.MapFrom(origin => origin.Policy.Value));

        CreateMap<User, UserViewModel>()
              .ForMember(x => x.RegistrationDate, opt => opt.MapFrom(origin => origin.RegistrationDate))
              .ForMember(x => x.IsActiveTitle, opt => opt.MapFrom(origin => origin.IsActive == true ? "فعال" : "غیرفعال"))
              .ForMember(x => x.AliasName, opt => opt.MapFrom(origin => !string.IsNullOrWhiteSpace(origin.AliasName) ? origin.AliasName + " (" + origin.FullName + "/" + origin.NID + ")" : origin.FullName + "/" + origin.NID))
              .ForMember(x => x.DisplayName, opt => opt.MapFrom(origin => !string.IsNullOrWhiteSpace(origin.AliasName) ? origin.AliasName + " (" + origin.FullName + "/" + origin.NID + ")" : origin.FullName + "/" + origin.NID));

        CreateMap<UserViewModel, UserExcelModel>()
            .ForMember(x => x.IsActiveTitle, opt => opt.MapFrom(origin => origin.IsActive == true ? "فعال" : "غیرفعال"));

       


        CreateMap<UserInRole, UserInRoleViewModel>()
            .ForMember(x => x.ExpirationDate, opt => opt.MapFrom(origin => origin.ExpiryDate != null ? CalenderWidget.ToJalaliDateTime((DateTime)origin.ExpiryDate) : "دائمی"));



        CreateMap<Role, RoleViewModel>();
        
        CreateMap<RoleClaim, RoleClaimViewModel>();

        CreateMap<PolicyParameterViewModel, PolicyParameterExcelModel>();
        
        CreateMap<PolicyViewModel, PolicyExcelModel>();
        
        CreateMap<UserPolicyViewModel, UserPolicyExcelModel>();

      

        CreateMap<UserInRoleViewModel, UserInRoleExcelModel>();
        CreateMap<RoleViewModel, RoleExcelModel>();
        CreateMap<RoleClaimViewModel, RoleClaimExcelModel>();

        CreateMap<Page, PageViewModel>();
        CreateMap<PageViewModel, PageExcelModel>();
        //===============================================
        //===============================================
        //===============================================Media
        CreateMap<MediaType,MediaTypeViewModel>();

        //===============================================
        //===============================================
        //===============================================Blog

        CreateMap<BlogType, BlogTypeViewModel>();
        CreateMap<BlogStatusType , BlogStatusTypeViewModel>();
        CreateMap<Blog , BlogViewModel>();
        CreateMap<BlogPublisher , BlogPublisherViewModel>();
        CreateMap<BlogOccupation , BlogOccupationViewModel>();

        //===============================================
        //===============================================
        //===============================================Publish

        CreateMap<PublishType , PublishTypeViewModel>();
        CreateMap<Publisher , PublisherViewModel>();
        CreateMap<PublisherBranch , PublisherBranchViewModel>();


        //===============================================
        //===============================================
        //===============================================Product

        CreateMap<ProductType,ProductTypeViewModel>();
        CreateMap<Product, ProductViewModel>();
     
           


        CreateMap<ProductCategoryType , ProductCategoryTypeViewModel>();
        CreateMap<ProductLable , ProductLableViewModel>();
        CreateMap<ProductComment , ProductCommentViewModel>();

        CreateMap<ProductOccupation, ProductOccupationViewModel>()
             .ForMember(x => x.ProductTitle, opt => opt.MapFrom(origin => origin.Product.Title))
             .ForMember(x => x.OccupationTitle, opt => opt.MapFrom(origin => origin.PersonOccupation.Occupation.Title))
             .ForMember(x => x.OccupationID, opt => opt.MapFrom(origin => origin.PersonOccupation.OccupationID))
             .ForMember(x => x.PersonID, opt => opt.MapFrom(origin => origin.PersonOccupation.PersonID))
             .ForMember(x => x.FullName, opt => opt.MapFrom(origin => origin.PersonOccupation.Person.FullName));




        CreateMap<ProductPublisher , ProductPublisherViewModel>();
        CreateMap<ProductPropertyType , ProductPropertyTypeViewModel>();
        CreateMap<ProductGenreType,ProductGenreTypeViewModel>();

        CreateMap<PropertyType , PropertyTypeViewModel>();
        CreateMap<GenreType, GenreTypeViewModel>();

        //===============================================
        //===============================================
        //===============================================Occupation

        CreateMap<Occupation , OccupationViewModel>();

        //===============================================
        //===============================================
        //===============================================People

        CreateMap<Person , PersonViewModel>();
        CreateMap<PersonOccupation , PersonOccupationViewModel>();
        CreateMap<PersonType , PersonTypeViewModel>();

        //===============================================
        //===============================================
        //===============================================Lable

        CreateMap<Lable , LableViewModel>();

        //===============================================
        //===============================================
        //===============================================Language

        CreateMap<Language , LanguageViewModel>();

        //===============================================
        //===============================================
        //===============================================Category

        CreateMap<CategoryType , CategoryTypeViewModel>();

        //===============================================
        //===============================================
        //===============================================Size

        CreateMap<SizeType , SizeTypeViewModel>();

        //===============================================
        //===============================================
        //===============================================Cover

        CreateMap<CoverType, CoverTypeViewModel>();

        //=============================================================== Notification
        CreateMap<Notification, NotificationViewModel>()
             .ForMember(x => x.NotificationTypeTitle, opt => opt.MapFrom(origin => origin.NotificationType.Title));

        CreateMap<NotificationDraft, NotificationDraftViewModel>();
        CreateMap<NotificationType, NotificationTypeViewModel>();
        CreateMap<NotificationOwner, NotificationOwnerViewModel>();
    }
}