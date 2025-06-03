// ProtectedRoute.js
import { useRouter } from 'next/navigation';
import { useAuth } from './AuthContext'; // Adjust path

const ProtectedRoute = ({ children }) => {
    const { user, isLoading } = useAuth();
    const router = useRouter();

    useEffect(() => {
        if (!isLoading && !user) {
            router.push('/login'); // Or wherever your login page is
        }
    }, [user, isLoading, router]);

    if (isLoading) {
        return <p>Loading...</p>; // Or a layout-preserving spinner
    }

    if (!user) {
        return null; // Or redirecting, so null while redirect happens
    }

    return children;
};

export default ProtectedRoute;

// Usage for /my-reservations page component:
// function MyReservationsPage() { /* ... content ... */ }
// export default () => <ProtectedRoute><MyReservationsPage /></ProtectedRoute>;